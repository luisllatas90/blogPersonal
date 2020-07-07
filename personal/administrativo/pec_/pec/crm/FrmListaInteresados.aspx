<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaInteresados.aspx.vb"
    Inherits="administrativo_pec_crm_FrmListaInteresados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv='X-UA-Compatible' content='IE=11' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <title>Lista de Interesados jQuery, Ajax y Bootstrap </title>
    <!-- Latest compiled and minified CSS -->
    <%--<link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../../assets/css/material.css'/>
    <link href="../../../assets/css/style.css?x=1" rel="stylesheet" type="text/css" />
    <link href="../../../assets/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <%--<script src="JQuery-1.12.4/jquery-1.12.4.min.js" type="text/javascript"></script>--%>
    <%--<script src="../../../assets/js/jquery.js" type="text/javascript"></script>--%>
    <link href="../../../assets/css/jtree.css" rel="stylesheet" type="text/css" />

    <script src="JQuery-1.12.4/jquery-1.12.4.min.js" type="text/javascript"></script>

    <!-- Latest compiled and minified JavaScript -->
    <%--<script src="bootstrap-3.3.7-dist/js/bootstrap.min.js" type="text/javascript"></script>--%>

    <script src="../../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../../assets/js/noty/notifications-custom.js"></script>

    <script src="../../../assets/js/tree-view/tree.js" type="text/javascript"></script>

    <script src="../../../assets/js/jquery.dataTables.min.js" type="text/javascript"></script>

    <script src="../../../assets/js/funciones.js" type="text/javascript"></script>

    <script src="js/Interesados.js?x=3332" type="text/javascript"></script>

    <style type="text/css">
        /* Centrar Modal de M... en IE *//*
        #CentraModal
        {*//*display: block; /* Es necesario mostrarlo como un bloque *//*position: absolute; /* Esto es obligatorio solo cuando sea una división principal *//*margin: auto;
            right: 0px;
            left: 5%;
            top: 5%;
            bottom: 0px; /* Si definimos esta opción estará en el centro del navegador (centro de la pantalla) *//*width: 300px; /* Si esto no se define no se visualizará los resultados en el navegador (Internet Explorer) *//*}*//*--*//* Centrar Modal de M... en IE *//*
            #CentraModal2
        {*//*display: block; /* Es necesario mostrarlo como un bloque *//*overflow: auto;
            position: absolute; /* Esto es obligatorio solo cuando sea una división principal *//* margin: auto;
            right: 0px;
            left: 5%;
            top: 2%;
            bottom: 0px; /* Si definimos esta opción estará en el centro del navegador (centro de la pantalla) *//* width: 800px; /* Si esto no se define no se visualizará los resultados en el navegador (Internet Explorer) *//*}*/.control-label
        {
            font-weight: 400;
            letter-spacing: 0.25px;
            color: #707780;
            font-weight: bold;
            font-size: 13px;
            text-transform: capitalize;
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
    </style>
</head>
<body>
      <div class="piluku-preloader text-center">
         <!--<div class="progress">
            <div class="indeterminate"></div>
            </div>-->
         <div class="loader">Loading...</div>
      </div>
    <form id="form1" runat="server">
    <div class="col-md-12">
        <!-- panel -->
        <div class="panel panel-piluku">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <b>Listado de Interesados</b>
                    <%--<span class="panel-options">
								<a href="#" class="panel-refresh">
									<i class="icon ti-reload"></i> 
								</a>
								<a href="#" class="panel-minimize">
									<i class="icon ti-angle-up"></i> 
								</a>
								<a href="#" class="panel-close">
									<i class="icon ti-close"></i> 
								</a>
							</span>--%>
                </h3>
            </div>
            <div class="panel-body">
           
                <div class="row">
                    <div class="col-md-12">
                        <button type="button" id="btnnew" class="btn btn-success" data-toggle="modal" data-target=".modal2">
                            Agregar</button>
                        <div id="rpta_delete">
                        </div>
                        <!-- Datos ajax Final -->
                        <div id="PnlList">
                            <table class="display dataTable cell-border" id="TablaInteresados" width="98%">
                                <thead>
                                    <tr role="row">
                                        <th style="width: 15%">
                                            N° Documento
                                        </th>
                                        <th style="width: 50%">
                                            Apellidos y Nombres
                                        </th>
                                        <th style="width: 25%">
                                            E-mail
                                        </th>
                                        <th style="width: 5%" align="center">
                                            Editar
                                        </th>
                                        <th style="width: 5%" align="center">
                                            Quitar
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="TbInteresados" runat="server">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="tree well" id="arbol">
                        <%--<ul>
                            <li class="parent_li"><span title="Collapse this branch" class="parent"><i class="fa fa-folder-open">
                            </i>Parent</span> <a href=""></a>
                                <ul>
                                    <li class="parent_li"><span title="Collapse this branch" class="child"><i class="fa fa-chevron-circle-up">
                                    </i>Child</span> <a href=""></a>
                                        <ul>
                                            <li><span class="grandchild"><i class="fa fa-file"></i>Grand Child</span> <a href="">
                                            </a></li>
                                        </ul>
                                    </li>
                                    <li class="parent_li"><span title="Collapse this branch" class="child"><i class="fa fa-chevron-circle-up">
                                    </i>Child</span> <a href=""></a>
                                        <ul>
                                            <li><span class="grandchild"><i class="fa fa-file"></i>Grand Child</span> <a href="">
                                            </a></li>
                                            <li class="parent_li"><span title="Collapse this branch" class="grandchild"><i class="fa fa-chevron-circle-up">
                                            </i>Grand Child</span> <a href=""></a>
                                                <ul>
                                                    <li class="parent_li"><span title="Collapse this branch" class="great-grandchild"><i
                                                        class="fa fa-chevron-circle-up"></i>Great Grand Child</span> <a href=""></a>
                                                        <ul>
                                                            <li><span class="greatgrand-grandchild"><i class="fa fa-file"></i>Great great Grand
                                                                Child</span> <a href=""></a></li>
                                                            <li><span class="greatgrand-grandchild"><i class="fa fa-file"></i>Great great Grand
                                                                Child</span> <a href=""></a></li>
                                                        </ul>
                                                    </li>
                                                    <li><span class="great-grandchild"><i class="fa fa-file"></i>Great Grand Child</span>
                                                        <a href=""></a></li>
                                                    <li><span class="great-grandchild"><i class="fa fa-file"></i>Great Grand Child</span>
                                                        <a href=""></a></li>
                                                </ul>
                                            </li>
                                            <li><span class="grandchild"><i class="fa fa-file"></i>Grand Child</span> <a href="">
                                            </a></li>
                                        </ul>
                                    </li>
                                </ul>
                            </li>
                            <li class="parent_li"><span title="Collapse this branch" class="parent"><i class="fa fa-folder-open">
                            </i>Parent</span> <a href=""></a>
                                <ul>
                                    <li><span class="child"><i class="fa fa-file"></i>Child</span> <a href=""></a></li>
                                </ul>
                            </li>
                            <li class="parent_li"><span title="Collapse this branch" class="parent"><i class="fa fa-folder-open">
                            </i>Parent</span> <a href=""></a>
                                <ul>
                                    <li><span class="child"><i class="fa fa-file"></i>Child</span> <a href=""></a></li>
                                </ul>
                            </li>
                        </ul>--%>
                    </div>
                </div>
            </div>
        </div>
        <!-- /panel -->
    </div>
    </form>
    <%-------------------------- Formulario Eliminar -------------------------------------%>
    <form id="eliminarDatos">
    <div class="modal fade bs-example-modal-sm" id="dataDelete" tabindex="-1" role="dialog"
        aria-labelledby="mySmallModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <input type="hidden" id="id_int" name="id_int" />
                    <h4 class="text-center text-muted">
                        ¿Estas seguro?</h4>
                    <p class="lead text-muted text-center" style="display: block; margin: 10px; font-size: 13px">
                        El Registro se eliminará de forma permanente, ¿Deseas continuar?</p>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-default" data-dismiss="modal">
                            Cancelar</button>
                        <a id="btnDelete" onclick="Quitar();" class="btn btn-primary">Aceptar</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    <%-------------------------- Fin Formulario Eliminar -------------------------------------%>
    <%-------------------------- Formulario Guardar/Actualizar -------------------------------------%>
    <div class="modal fade modal2" id="dataRegister" role="dialog" data-keyboard="false"
        data-backdrop="static" aria-labelledby="myModalLabel" style="overflow-y: auto;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
             <div id="NotyModal"></div>
                <%-- <div class="modal-header" style="height:70px;">
                   <div class="panel panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Interesados
                                <span class="panel-options">
						<a class="panel-refresh" href="#">
							<i class="icon ti-reload"></i>
						</a>
						<a class="panel-minimize" href="#">
							<i class="icon ti-angle-up"></i>
						</a>
						<a class="panel-close" href="#">
							<i class="icon ti-close"></i>
						</a>
					</span>
                            </h3>
                        </div>
                        
                        <div class="row" style="height: 30px">
                        <div class="col-sm-12">
                            <h4 class="text-primary">
                                Mantenimiento de Interesados</h4>
                        </div>
                    </div>
                    
                    </div>
                </div>--%>
                <div class="modal-body">
                    <div id="rootwizard1">
                        <div class="navbar">
                            <div class="navbar-inner">
                                <div class="container" style="width: 100%">
                                    <ul class="piluku-tabs piluku-noborder nav-justified nav nav-pills">
                                        <li class="active"><a aria-expanded="true" href="#tabs1" data-toggle="tab">Registro</a>
                                        </li>
                                        <li><a href="#tabs2" data-toggle="tab">Comunicación</a> </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <%--  <div class="progress" id="bar">
                            <div class="progress-bar progress-bar-primary active" role="progressbar" aria-valuenow="0"
                                aria-valuemin="0" aria-valuemax="100" style="width: 16.66%;">
                            </div>
                        </div>--%>
                        <div class="tab-content piluku-tab-content">
                            <div class="tab-pane active" id="tabs1">
                                <form id="guardarDatos">
                                <div id="Paso1">
                                    <div class="row">
                                        <div class="form-group">
                                            <input type="hidden" id="codigo_int" name="codigo_int" value="0" />
                                            <input type="hidden" id="Paso" name="Paso" value="1" />
                                            <label for="cboTipoDocumento" class="control-label col-sm-3">
                                                Tipo Documento:</label>
                                            <div class="col-sm-4">
                                                <select id="cboTipoDocumento" name="cboTipoDocumento" class="form-control input-sm"
                                                    runat="server">
                                                    <option value='0'>--Seleccione--</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="numdoc" class="control-label col-sm-3">
                                                N° Documento:</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control input-sm" id="numdoc" name="numdoc" runat="server"
                                                    maxlength="12" />
                                            </div>
                                            <div class="col-sm-2">
                                                <input type="button" class="btn btn-warning" id="btnbuscar" value="Buscar" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="apepat" class="control-label col-sm-3">
                                                Apellido Paterno:</label>
                                            <div class="col-sm-4">
                                                <input type="text" class="form-control input-sm " id="apepat" name="apepat" runat="server"
                                                    required maxlength="40" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="apemat" class="control-label col-sm-3">
                                                Apellido Materno:</label>
                                            <div class="col-sm-4">
                                                <input type="text" class="form-control input-sm" id="apemat" name="apemat" required
                                                    maxlength="40" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="nombres" class="control-label col-sm-3">
                                                Nombres:</label>
                                            <div class="col-sm-4">
                                                <input type="text" class="form-control input-sm" id="nombres" name="nombres" required
                                                    maxlength="50" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="cbodepartamento" class="control-label col-sm-3">
                                                Departamento:</label>
                                            <div class="col-sm-4">
                                                <select id="cbodepartamento" name="cbodepartamento" class="form-control input-sm"
                                                    runat="server">
                                                    <option value='0'>--Seleccione--</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="cboprovincia" class="control-label col-sm-3">
                                                Provincia:</label>
                                            <div class="col-sm-4">
                                                <select id="cboprovincia" name="cboprovincia" class="form-control input-sm" runat="server">
                                                    <option value='0'>--Seleccione--</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="cbodistrito" class="control-label col-sm-3">
                                                Distrito:</label>
                                            <div class="col-sm-4">
                                                <select id="cbodistrito" name="cbodistrito" class="form-control input-sm" runat="server">
                                                    <option value='0'>--Seleccione--</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="direccion" class="control-label col-sm-3">
                                                Dirección:</label>
                                            <div class="col-sm-5">
                                                <input type="text" class="form-control input-sm" id="direccion" name="direccion"
                                                    maxlength="50" />
                                            </div>
                                            <label for="telefono" class="control-label col-sm-2">
                                                Teléfono Fijo</label>
                                            <div class="col-sm-2">
                                                <input type="text" class="form-control input-sm" id="telefono" name="telefono" required
                                                    maxlength="10" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="email" class="control-label col-sm-3">
                                                Correo:</label>
                                            <div class="col-sm-5">
                                                <input type="text" class="form-control input-sm" id="email" name="email" maxlength="50" />
                                            </div>
                                            <label for="celular" class="control-label col-sm-1">
                                                Celular:</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control input-sm" id="celular" name="celular" required
                                                    maxlength="9" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="procedencia" class="control-label col-sm-3">
                                                Procedencia:</label>
                                            <div class="col-sm-5">
                                                <input type="text" class="form-control input-sm" id="procedencia" name="procedencia"
                                                    maxlength="50" />
                                            </div>
                                            <label for="grado" class="control-label col-sm-1">
                                                Grado:</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control input-sm" id="grado" name="grado" maxlength="40" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="carrera" class="control-label col-sm-3">
                                                Carrera Profesional:</label>
                                            <div class="col-sm-6">
                                                <input type="text" class="form-control input-sm" id="carrera" name="carrera" required
                                                    maxlength="40" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12 h5" style="height: 45px">
                                            <div id="rpta_registro">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </form>
                            </div>
                            <div class="tab-pane" id="tabs2">
                                <div id="Paso2">
                                    <form id="FormPaso2">
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="carrera_2" class="control-label col-sm-3">
                                                Carrera Prof. en Consulta:</label>
                                            <div class="col-sm-9">
                                                <input type="text" class="form-control input-sm" id="carrera_2" name="carrera_2"
                                                    required maxlength="40" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="cboTipoComunicacion" class="control-label col-sm-3">
                                                Tipo de Comunicación:</label>
                                            <div class="col-sm-9">
                                                <select id="cboTipoComunicacion" name="cboTipoComunicacion" class="form-control input-sm"
                                                    runat="server">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="txtcomunicacion" class="control-label col-sm-3">
                                                Detalle de Comunicación:</label>
                                            <div class="col-sm-9" style="text-align: center">
                                                <textarea id="txtcomunicacion" name="txtcomunicacion" class="form-control" style="width: 99%"
                                                    rows="3"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-9 h5" style="height: 40px">
                                                <div id="rpta_comunicacion">
                                                </div>
                                            </div>
                                            <div class="col-sm-3 text-right">
                                                <button type="button" id="btnAgregaComunicacion" onclick="Guardar_Com()" class="btn btn-success">
                                                    Agregar</button>
                                            </div>
                                        </div>
                                        <div style="float:left;" id="divLoading" class="hidden"><img id="imgload" src="../../../assets/images/loading.GIF" ></div>
                                    </div>
                                    <div class="row" style="overflow-y: auto; height: 270px">
                                        <%--<div class="row">--%>
                                        <div class="col-sm-12">
                                            <table id="TablaComunicacion" class="display dataTable cell-border" width="98%">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 5%">
                                                            N°
                                                        </th>
                                                        <th style="width: 20%">
                                                            Carrera
                                                        </th>
                                                        <th style="width: 20%">
                                                            Tipo Comunicacion
                                                        </th>
                                                        <th style="width: 40%">
                                                            Detalle
                                                        </th>
                                                        <th style="width: 10%; text-align: center">
                                                            Fecha
                                                        </th>
                                                        <th style="width: 5%; text-align: center">
                                                            Quitar
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody id="TbComunicacion" class="small" runat="server">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                        <%-- <ul class="pager wizard">
                                <li class="previous first disabled" style="display: none;"><a href="#">First</a>
                                </li>
                                <li class="previous disabled"><a href="#">Previous</a> </li>
                                <li class="next last" style="display: none;"><a href="#">Last</a> </li>
                                <li class="next"><a href="#">Next</a> </li>
                            </ul>--%>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <button type="button" id="btnCancelar" onclick="Limpiar();" class="btn btn-default"
                                    data-dismiss="modal">
                                    Cancelar</button>
                                <button type="button" id="btnAtras" class="btn btn-default" style="display: none;">
                                    Atras</button>
                                <button type="button" id="btnGuardar" onclick="Guardar();" class="btn btn-primary">
                                    Aceptar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-------------------------- Formulario Eliminar Comunicacion-------------------------------------%>
    <form id="FormEliminaCom">
    <div class="modal fade modal3" id="DataDeleteCom" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <input type="hidden" id="id_com" name="id_com" />
                    <h4 class="text-center text-muted">
                        ¿Estas seguro?</h4>
                    <p class="lead text-muted text-center" style="display: block; margin: 10px; font-size: 13px">
                        La Comunicación se eliminará de forma permanente, ¿Deseas continuar?</p>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-default" data-dismiss="modal">
                            Cancelar</button>
                        <a id="A1" onclick="QuitarCom();" class="btn btn-primary">Aceptar</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    <%-------------------------- Fin Formulario Eliminar Comunicacion-------------------------------------%>

</body>
</html>
