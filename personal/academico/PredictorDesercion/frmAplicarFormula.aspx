<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAplicarFormula.aspx.vb" Inherits="academico_PredictorDiserccion_frmAplicarFormula" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">
<head>
    <title id='Description'>jqxGrid supports custom Filter Menus. To create such menus, you will have to override the built-in Filter Menu by implmenting the "createfilterpanel" function.
    </title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
     <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <link rel="stylesheet" href="../../assets/jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link href="../../assets/jqwidgets/jqwidgets/styles/jqx.light.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../assets/jqwidgets/scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src='../../assets/js/bootstrap.min.js'></script>
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


    <script src="js/AplicarFormula.js" type="text/javascript"></script>
    <style>
        .form-group {
            margin-bottom: 2px;
        }
    </style>
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
                                            <i class="icon ti-volume page_header_icon"></i><span class="main-text">Analisis Riesgo de deserción</span>
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
                                                    <label class="col-sm-12 col-md-3 control-label">Fecha</label>
                                                    <div class="col-sm-12 col-md-5">
                                                       <div id='txtFecha'>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="form-group">
                                                    <label class="col-sm-12 col-md-3 control-label">Formula</label>
                                                    <div class="col-sm-12 col-md-5">
                                                        <select name="cboFormula" class="form-control" id="cboFormula">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                    <div class="col-sm-2 col-md-2">
                                                        <input type="button" class="btn btn-success" id="jqxConsultar" value="Aplicar" />
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
                    <div class="panel panel-piluku" id="PanelLista">
                       <div class="panel-heading">
                            <h3 class="panel-title">
                               Listado <span class="panel-options"><a class="panel-refresh" href="#">
                                <i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                             
                             <div class="col-sm-12 col-md-12" id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
                                <div id="jqxgrid">
                                </div>                                 
                            </div>
                        </div>
                    </div>
                 </div>
             </div>
        </div>
    </div>
   <div class="modal fade" id="modDetalle" role="dialog" aria-labelledby="myModalLabel" style='z-index:300' aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" id="modalReg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                    </button>
                    <h4 id="tituloFicha" class="modal-title" id="myModalLabel3">
                       Ficha del Estudiante</h4>
                </div>
                <div class="modal-body">
                    <form class="form form-horizontal" id="Form1" onsubmit="return false;" action="#" method="post">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Desertor</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="DESERTOR" name="DESERTOR"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Tasa de Faltas</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="TASAFALT" name="TASAFALT"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Matrículas</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="NMATRIC" name="NMATRIC"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Tasa de Presencias</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="TASAPRES" name="TASAPRES"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Discontinuo</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="DISCONTINUO" name="DISCONTINUO"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Nivel Entrada</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="NIVELENTRADA" name="NIVELENTRADA"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Tasa Aprobación</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="TASAAAPRO" name="TASAAAPRO"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Tasa desaprobación</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="TASADESA" name="TASADESA"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Deudas Pagadas</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="DEUDASPAGA" name="DEUDASPAGA"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Deudas Pendientes</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="DEUDASPEND" name="DEUDASPEND"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Deudas Atrasadas</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="DEUDASATRASADAS" name="DEUDASATRASADAS"/>
                                        </div>
                                         
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Examen Entrada</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="EXAMENTRA" name="EXAMENTRA"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Vocacional</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="VOCACIONAL" name="VOCACIONAL"/>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Ansiedad</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="ANCIEDAD" name="ANCIEDAD"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Depresión</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="DEPRESION" name="DEPRESION"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Autoeficacia</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="AUTOEFICACIA" name="AUTOEFICACIA"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Trabaja</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="TRABAJA" name="TRABAJA"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Dependientes</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="DEPENDIENTES" name="DEPENDIENTES"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Prob. Familiares</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="PROBFAMIL" name="PROBFAMIL"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Prob. Económicos</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="PECONOM" name="PECONOM"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Prob. Salud</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="PSALUD" name="PSALUD"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Prob.Académicos</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="PACADEM" name="PACADEM"/>
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Prom. Ponderado</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="PROMEPOND" name="PROMEPOND"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Notas Aprobadas</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="NOTASAPRO" name="NOTASAPRO" />
                                        </div>
                                         <label class="col-sm-4 col-md-4 control-label">Notas Desaprobadas</label>
                                        <div class="col-sm-2 col-md-2">
                                              <input type=text class="form-control" id="NOTASDESAP" name="NOTASDESAP"/>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-3 col-md-3 control-label">Total Notas</label>
                                        <div class="col-sm-1 col-md-2">
                                             <input type=text class="form-control" id="TOTALNOTAS" name="TOTALNOTAS"/>
                                        </div>
                                         
                                    </div>
                                </div>
                            </div>
                        </form>
                </div>
                <div class="modal-footer">
                    <center>
                        
                        <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                            Salir</button>
                    </center>
                </div>
            </div>
        </div>
    </div>
</body>
</html>