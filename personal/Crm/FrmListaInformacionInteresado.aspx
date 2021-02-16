<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaInformacionInteresado.aspx.vb"
    Inherits="Crm_FrmListaInformacionInteresado" %>

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
    <link rel="stylesheet" href="../assets/jquery-timepicker/jquery.timepicker.min.css">
    
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script type="text/javascript" src='../assets/js/app.js'></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap.min.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.nicescroll.min.js'></script>

    <script type="text/javascript" src='../assets/js/wow.min.js'></script>

    <script type="text/javascript" src="../assets/js/jquery.nicescroll.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>
    
    <!-- <script type="text/javascript" src='../assets/bootstrap-datetimepicker/js/moment.js'></script> -->
    
    <script type="text/javascript" src="../assets/jquery-timepicker/jquery.timepicker.min.js"></script>

    <%--
    <script type="text/javascript" src='../assets/js/jquery.accordion.js'></script>

    <script type="text/javascript" src='../assets/js/materialize.js'></script>

    <script type="text/javascript" src='../assets/js/bic_calendar.js'></script>

    <script type="text/javascript" src='../assets/js/core.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.countTo.js'></script>--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    <%--<script type="text/javascript" src='../assets/js/form-elements.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/select2.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>--%>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap-datepicker.js'></script>

    <script type="text/javascript" src="js/crm.js?x=40"></script>

    <script type="text/javascript" src="js/JsInformacionInteresado.js?x=221"></script>

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
        .info-logo-content ul
        {
            padding: 5px 5px 0px;
        }
        .i-am-new
        {
            z-index: 100;
        }
        .no-gutters {
            margin-right: 0;
            margin-left: 0;
        }

        .no-gutters > [class*="col-"] {
            padding-right: 0;
            padding-left: 0;
        }
        
        
        #tbComunicacion tr.unselect td {
            background-color: white;
            font-weight: normal;
        }
        
        #tbComunicacion tr.select td {
            background-color: #f2f1b1;
            font-weight: bold;
        }

        #tbComunicacion .btn {
            padding: 3px 9px;
        }

        #msgVerifiCallCenter {
            color: #c73535;
            font-weight: bold;
            margin-top: 0px;
        }

        .div-disabled {
            pointer-events: none;
            opacity: 0.7;
        }
    </style>
</head>
<body>
    <div class="piluku-preloader text-center hidden">
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper " style="min-height: 800px;">
        <div class="content" id="content">
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
                                            <!-- <span class="main-text">Información de Interesado</span> -->
                                            <ul class="list-inline list-unstyled">
                                                <i class="icon ti-folder page_header_icon"></i>
                                                <li style="font-weight: 680">
                                                    <label id="lblInteresado" runat="server" style="font-size: 18px;">
                                                    </label>
                                                    <p style="border-top: 1px solid #dad7d7;">
                                                        <label id="lblInteres" runat="server" style="font-size: 14px; margin-bottom: 0px"></label>
                                                        <label id="lblOtrasCarreras" runat="server" style="font-size: 10px;"></label>
                                                    </p>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <a class="btn btn-primary" id="btnRegresar" name="btnRegresar" onclick="fnRegresar()"
                                                    href="#"><i class="ion-forward"></i>&nbsp;Regresar</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- <br>
            <div class="col-md-6 nopad-right">
                <div class="piluku-panel no-pad panel">
                    <form id="frmInteresado" name="frmInteresado" enctype="multipart/form-data" class="form-horizontal"
                    method="post" onsubmit="return false;" action="#">
                    </form>
                    <div class="ios-profile-widget">
                        <div class="header_cover">
                            <img src="../assets/images/avatar/one.png" alt="">
                            <h3>
                                Roger Vera</h3>
                            <i class="ion ion-social-twitter">@RVera</i>
                            <br />
                            <i class="ion ion-social-facebook">RoggerVera</i>
                        </div>
                        <!-- cover -->
                        <ul class="list-group">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">
                                        DNI:</label>
                                    <div class="col-sm-7">
                                        <input type="text" id="txtDNI" name="txtDNI" class="form-control" disabled /></div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">
                                            Apellido Paterno:</label>
                                        <div class="col-sm-7">
                                            <input type="text" id="txtApellidoPat" name="txtApellidoPat" class="form-control"
                                                disabled />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">
                                                Apellido Materno:</label>
                                            <div class="col-sm-7">
                                                <input type="text" id="txtApellidoMat" name="txtApellidoMat" class="form-control"
                                                    disabled />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">
                                                Nombres:</label>
                                            <div class="col-sm-7">
                                                <input type="text" id="txtNombres" name="txtNombres" class="form-control" disabled />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">
                                                Fecha Nacimiento:</label>
                                            <div class="col-sm-7">
                                                <label class="col-sm-12 control-label">
                                                    02/05/1999</label></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">
                                                Dirección:</label>
                                            <div class="col-sm-7">
                                                <input type="text" id="txtDireccion" name="txtDireccion" class="form-control" disabled />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">
                                                Teléfono:</label>
                                            <div class="col-sm-7">
                                                <input type="text" id="txtTelefono" name="txtTelefono" class="form-control" disabled />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">
                                                Correo Electrónico:</label>
                                            <div class="col-sm-7">
                                                <input type="text" id="txtEmail" name="txtEmail" class="form-control" disabled />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">
                                                Institución Educativa:</label>
                                            <div class="col-sm-7">
                                                <input type="text" id="txtInstitucionEducativa" name="txtInstitucionEducativa" class="form-control"
                                                    disabled />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">
                                                Carrera Profesional:</label>
                                            <div class="col-sm-7">
                                                <input type="text" id="txtCarreraProfesional" name="txtCarreraProfesional" class="form-control"
                                                    disabled />
                                            </div>
                                        </div>
                                        <input type="hidden" id="hdcod" name="hdcod" value="0">
                        </ul>
                    </div>
                    <!-- ios-profile -->
                </div>
                <!-- panel -->
            </div> --%>
            <div style="overflow-x: auto;">
                <table class="table-responsive" style="display: inline-table; float:left; width: 100%">
                    <tbody>
                        <tr valign="top" style="height: 0">
                            <td>
                                <div class="col-md-12 col-xs-12 nopad-right">
                                    <br>
                                    <div class="panel panel-piluku">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">
                                                Datos Personales
                                            </h3>
                                        </div>
                                        <div class="panel-body no-padding">
                                            <form id="frmInteresado" name="frmInteresado" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                                <input type="hidden" id="hdcodint" name="hdcodint" value="0" />
                                                <input type="hidden" id="hdFiltros" name="hdFiltros" value="0" />
                                                <input type="hidden" id="hdCodigoTest" name="hdCodigoTest" value="0" />
                                                <input type="hidden" id="hdCodigoCon" name="hdCodigoCon" value="0" />
                                                <input type="hidden" id="hdCodigoEveIni" name="hdCodigoEveIni" value="0" />
                                                <input type="hidden" id="hdCodigoEve" name="hdCodigoEve" value="0" />
                                                <input type="hidden" id="hdCodigoOri" name="hdCodigoOri" value="0" />
                                                <input type="hidden" id="hdGrado" name="hdGrado" value="0" />
                                            </form>
                                            <div class="row" style="font-size: 12px">
                                                <div class="col-md-12 col-xs-12 col-sm-12">
                                                    <div class="info-logo-content">
                                                        <div id="datos" name="datos">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                
                <table class="table-responsive" style="display: inline-table; float:left; width: 50%">
                    <tr id="trRequisitos">
                        <td>
                            <div class="col-md-12 col-xs-12 nopad-right">
                                <div class="panel panel-piluku">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Ingresante: Asistencia de proceso de admisión</h3>
                                    </div>
                                    <div class="panel-body no-padding">
                                        <div class="table-responsive" style="font-size: 12px; font-weight: 200;">
                                            <table id="tRequisitos" name="tRequisitos" class="display dataTable cell-border" width="100%" style="width: 100%; font-size: 12px; font-weight: 200;">
                                                <thead>
                                                    <tr>
                                                        <th class="sorting_asc" tabindex="0" aria-sort="ascending">N°</th>
                                                        <th class="sorting" tabindex="0" aria-sort="ascending">Descripción</th>
                                                        <th class="sorting" tabindex="0" aria-sort="ascending">Fecha</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tbRequisitos">
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <th colspan="3" rowspan="1">
                                                        </th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr valign="top" style="height: 0;">
                        <td>
                            <div class="col-md-12 col-xs-12 nopad-right">
                                <!-- panel -->
                                <div class="panel panel-piluku">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">
                                            Comunicación</h3>
                                    </div>
                                    <div class="panel-body no-padding">
                                        <div class="row">
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <a class="btn btn-success" id="btnAgregarComunicacion" name="btnAgregarComunicacion"
                                                            href="#" data-toggle="modal" data-target="#mdComunicacion"><i class="ion-android-add">
                                                            </i>&nbsp;Agregar</a>
                                                            &nbsp;&nbsp;
                                                        <a class="btn btn-info" id="btnTelefonos" name="btnTelefonos"
                                                            href="#" data-toggle="modal" data-target="#mdTelefono"><i class="ion-android-search">
                                                            </i>&nbsp;Teléfonos</a>
                                                    </div>
                                                </div>
                                            </div>
                                            <form class="form form-horizontal" id="frmListaComunicacion" name="frmListaComunicacion"
                                            onsubmit="return false;" action="#" method="post">
                                            </form>
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                        <div id="tComunicacion_wrapper" class="dataTables_wrapper" role="grid">
                                                            <table id="tComunicacion" name="tComunicacion" class="display dataTable cell-border"
                                                                width="100%" style="width: 100%;">
                                                                <thead>
                                                                    <tr role="row">
                                                                        <td width="5%" style="font-weight: bold;" class="sorting_asc" tabindex="0"
                                                                            rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                            N.
                                                                        </td>
                                                                        <td width="8%" style="font-weight: bold;" class="sorting" tabindex="0"
                                                                            rowspan="1" colspan="1">
                                                                            Fecha
                                                                        </td>
                                                                        <td width="15%" style="font-weight: bold;" class="sorting" tabindex="0"
                                                                            rowspan="1" colspan="1">
                                                                            Motivo
                                                                        </td>
                                                                        <td width="15%" style="font-weight: bold;" class="sorting" tabindex="0"
                                                                            rowspan="1" colspan="1">
                                                                            Tipo Com.
                                                                        </td>
                                                                        <td width="29%" style="font-weight: bold;" class="sorting" tabindex="0"
                                                                            rowspan="1" colspan="1">
                                                                            Detalle
                                                                        </td>
                                                                        <td width="13%" style="font-weight: bold;" class="sorting" tabindex="0"
                                                                            rowspan="1" colspan="1">
                                                                            Usuario
                                                                        </td>
                                                                        <td width="30%" style="font-weight: bold; text-align: center" class="sorting"
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
                                                                <tbody id="tbComunicacion">
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /panel -->
                            </div>
                        </td>
                    </tr>
                    
                    <tr valign="top" style="height: 0;">
                        <td>
                            <div class="col-md-12 col-xs-12 nopad-right">
                                <!-- panel -->
                                <div class="panel panel-piluku">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">
                                            Acuerdos</h3>
                                    </div>
                                    <form class="form form-horizontal" id="frmListaAcuerdo" name="frmListaAcuerdo" onsubmit="return false;"
                                    action="#" method="post">
                                    </form>
                                    <div class="panel-body no-padding">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                        <div id="Div1" class="dataTables_wrapper" role="grid">
                                                            <table id="tAcuerdo" name="tAcuerdo" class="display dataTable cell-border" width="100%"
                                                                style="width: 100%;">
                                                                <thead>
                                                                    <tr role="row">
                                                                        <td width="5%" style="font-weight: bold; width: 40px;" class="sorting_asc" tabindex="0"
                                                                            rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                            N.
                                                                        </td>
                                                                        <td width="10%" style="font-weight: bold; width: 80px;" class="sorting" tabindex="0"
                                                                            rowspan="1" colspan="1">
                                                                            Fecha
                                                                        </td>
                                                                        <td width="10%" style="font-weight: bold; width: 80px;" class="sorting" tabindex="0"
                                                                            rowspan="1" colspan="1">
                                                                            Hora
                                                                        </td>
                                                                        <td width="45%" style="font-weight: bold; width: 200px;" class="sorting" tabindex="0"
                                                                            rowspan="1" colspan="1">
                                                                            Detalle
                                                                        </td>
                                                                        <td width="15%" style="font-weight: bold; width: 50px;" class="sorting" tabindex="0"
                                                                            rowspan="1" colspan="1">
                                                                            Usuario
                                                                        </td>
                                                                        <td width="15%" style="font-weight: bold; width: 30px; text-align: center" class="sorting"
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
                                                                <tbody id="tbAcuerdo">
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /row -->
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                
                <table class="table-responsive" style="display: inline-table; float:left; width: 50%">
                    <tbody>
                        <tr valign="top" style="height: 0">
                            <td>
                                <div class="col-md-12 col-xs-12 nopad-right">
		                            <div class="panel panel-piluku">
			                            <div class="panel-heading">
				                            <h3 class="panel-title">
					                            Historial
				                            </h3>
			                            </div>
			                            <div class="panel-body no-padding">
				                            <div class="table-responsive" style="font-size: 12px; font-weight: 200;">
					                            <table id="tHistorial" class="display dataTable cell-border" width="100%" style="width: 100%; font-size: 12px; font-weight: 200;">
						                            <thead>
							                            <tr>
								                            <th class="sorting_asc" tabindex="0" aria-sort="ascending">
									                            Ingreso
								                            </th>
								                            <th>
									                            Egreso
								                            </th>
								                            <th>
									                            Programa
								                            </th>
								                            <th>
									                            Modalidad
								                            </th>
								                            <th>
									                            Estado
								                            </th>
								                            <th>
									                            Deuda
								                            </th>
							                            </tr>
						                            </thead>
						                            <tbody id="tbHistorial">
						                            </tbody>
						                            <tfoot>
							                            <tr>
								                            <th colspan="6" rowspan="1">
								                            </th>
							                            </tr>
						                            </tfoot>
					                            </table>
				                            </div>
			                            </div>
		                            </div>
	                            </div>
                            </td>
                        </tr>

                        <tr valign="top" style="height: 0">
                            <td>
                                <div class="col-md-12 col-xs-12 nopad-right">
		                            <div class="panel panel-piluku">
			                            <div class="panel-heading">
				                            <h3 class="panel-title">
					                            Eventos
				                            </h3>
			                            </div>
			                            <div class="panel-body no-padding">
				                            <div class="table-responsive" style="font-size: 12px; font-weight: 200;">
					                            <table id="tEventos" class="display dataTable cell-border" width="100%" style="width: 100%; font-size: 12px; font-weight: 200;">
						                            <thead>
							                            <tr>
								                            <th class="sorting_asc" tabindex="0" aria-sort="ascending">
									                            Evento CRM
								                            </th>
								                            <th>
									                            Origen
								                            </th>
								                            <th>
									                            Fecha
								                            </th>
								                            <th>
									                            Usuario
								                            </th>
							                            </tr>
						                            </thead>
						                            <tbody id="tbEventos">
						                            </tbody>
						                            <tfoot>
							                            <tr>
								                            <th colspan="5" rowspan="1">
								                            </th>
							                            </tr>
						                            </tfoot>
					                            </table>
				                            </div>
			                            </div>
		                            </div>
	                            </div>
                            </td>
                        </tr>
                        
                        <tr valign="top" style="height: 0">
                            <td>
                                <div class="col-md-12 col-xs-12 nopad-right">
		                            <div class="panel panel-piluku">
			                            <div class="panel-heading">
				                            <h3 class="panel-title">
					                            Interés</h3>
			                            </div>
			                            <form id="frmListaInteres" name="frmListaInteres" enctype="multipart/form-data" class="form-horizontal" method="post" onsubmit="return false;" action="#">
				                            <div class="panel-body no-padding">
                                                <div class="row">
                                                    <div class="col-xs-12 col-sm-12">
                                                        <label for="cboOrigen" class="form-control-sm">Origen</label>
                                                        <select id="cboOrigen" name="cboOrigen" runat="server" style="width: 100%">
                                                            <option value="">--Seleccione--</option>
                                                        </select>
                                                        <hr style="margin-top: 7px;margin-bottom: 5px;">
                                                    </div>
                                                </div>
			                                    <div class="col-xs-12 col-sm-6">
                                                    <label for="cboTipoEstudio" class="form-control-sm">
                                                        Tipo Estudio</label>
                                                    <select id="cboTipoEstudio" name="cboTipoEstudio" runat="server" onchange="fnLlenarConvocatoriaCombo(this);" style="width: 100%">
                                                        <option value="">--Seleccione--</option>
                                                    </select>
                                                </div>
                                                <div class="col-xs-12 col-sm-6">
                                                    <label for="cboConvocatoria" class="form-control-sm">
                                                        Convocatoria</label>
                                                    <select id="cboConvocatoria" name="cboConvocatoria" runat="server" onchange="fnListarInteres(false);" style="width: 100%">
                                                        <option value="">--Seleccione--</option>
                                                    </select>
                                                    <label>
                                                        <input id="chkMarcar" type="checkbox" style="float: left; vertical-align:middle;" onclick="seleccionarTodo(this);" />
                                                        <span style="float: left; margin: 3px 0px 0px 3px; color:#54606f;">MARCAR/DESMARCAR TODOS</span>
                                                    </label>
                                                </div>
				                            
				                                <table id="tInteres" width="100%" style="width: 100%; border-top: 1px solid #e5e5e5;">
					                                <thead>
						                                <tr>
							                                <td>
								                                &nbsp;
							                                </td>
							                                <td>
								                                &nbsp;
							                                </td>
						                                </tr>
					                                </thead>
					                                <tbody id="tbInteres">
					                                </tbody>
				                                </table>
					                            
					                            <div class="panel-body" style="border-top: 1px solid #e5e5e5; margin-top: 10px;">
						                            <div class="row" style="text-align: center">
							                            <button id="btnActualizar" name="btnActualizar" class="btn btn-success" onclick="fnGuardarInteres();">
								                            Registrar interés
							                            </button>
							                            <button id="Button1" name="btnCancelar" class="btn btn-info" onclick="fnListarInteres(false);">
								                            Resetear
							                            </button>
						                            </div>
					                            </div>
				                            </div>
			                            </form>
		                            </div>
	                            </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            
            <!-- /panel -->
            <div class="row">
                <div class="modal fade" id="mdComunicacion" role="dialog" aria-labelledby="myModalLabel"
                    style="z-index: 0" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                </button>
                                <h4 class="modal-title" id="H1">
                                    Registrar Comunicación</h4>
                            </div>
                            <div class="modal-body">
                                <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Motivo:</label>
                                            <div class="col-sm-8">
                                                <select class="form-control" id="cboMotivoR" name="cboMotivoR">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Tipo de Comunicación:</label>
                                            <div class="col-sm-7">
                                                <select class="form-control" id="cboTipoComunicacionR" name="cboTipoComunicacionR">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                            <div class="col-sm-1">
                                                <button id="chat-wsp" class="btn btn-success btn-sm" style="padding: 3px 8px;">
                                                    <i class="ion-android-send"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Estado de Comunicación:</label>
                                            <div class="col-sm-8">
                                                <select class="form-control" id="cboEstadoComunicacion" name="cboEstadoComunicacion">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row collapse" id="rowEvento">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Evento:</label>
                                            <div class="col-sm-8">
                                                <select class="form-control" id="cboEvento" name="cboEvento">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row collapse" id="rowNrosCallcenter">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" id="lblNroAnexo">N° Anexo:</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtNroAnexo" name="txtNroAnexo" class="form-control only-digits" readonly="readonly" />
                                            </div>
                                            <label class="col-sm-2 control-label" id="lblNroInteresado">N° Interesado:</label>
                                            <div class="col-sm-4">
                                                <div class="row no-gutters">
                                                    <div class="col-sm-6">
                                                        <select name="cboNroInteresado" id="cboNroInteresado" class="form-control"></select>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <input type="text" id="txtNroInteresado" name="txtNroInteresado" class="form-control only-digits" readonly="readonly" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-3 col-sm-offset-3">
                                                <label style="display: flex; margin-top: 6px;" id="lblChkNroAnexo">
                                                    <input type="checkbox" id="chkNroAnexo" class="chkNroAnexo" style="margin-right: 5px;">Modificar Número
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row collapse" id="rowNrosMensaje">
                                        <div class="col-sm-9 col-sm-offset-3">
                                            <h5 id="msgVerifiCallCenter">La llamada ya ha sido validada con los registros de central telefónica</h5>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Detalle:</label>
                                            <div class="col-sm-8">
                                                <textarea id="txtdetalle" name="txtdetalle" style="width: 100%" rows="5"></textarea>
                                                <br />
                                                <div style="text-align: right; font-weight: bold;" id="Contador_Com">
                                                    0 Caracteres</div>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                                <%--<div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                <div id="Div4" class="dataTables_wrapper" role="grid">
                                                    <div class="dataTables_filter pull-right" id="Div6">
                                                        <label>
                                                            Buscar:<input type="text" aria-controls="tAcuerdo"></label>
                                                    </div>
                                                    <table id="tComunicacion" class="display dataTable cell-border" width="100%" style="width: 100%;">
                                                        <thead>
                                                            <tr role="row">
                                                                <td width="5%" style="font-weight: bold; width: 54px;" class="sorting_asc" tabindex="0"
                                                                    rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                    N.
                                                                </td>
                                                                <td width="20%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                    rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                    Acuerdo
                                                                </td>
                                                                <td width="40%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                    rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                    Detalle
                                                                </td>
                                                                <td width="15%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                    rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                                    Fecha
                                                                </td>
                                                                <td width="10%" style="font-weight: bold; width: 203px;" class="sorting" tabindex="0"
                                                                    rowspan="1" colspan="1" aria-label="Detalle: activate to sort column ascending">
                                                                    Estado
                                                                </td>
                                                                <td width="10%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
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
                                                        <tbody id="Tbody1" role="alert" aria-live="polite" aria-relevant="all">
                                                            <tr class="odd">
                                                                <td>
                                                                    1
                                                                </td>
                                                                <td>
                                                                    Llamar 10-05-2017
                                                                </td>
                                                                <td>
                                                                    Interesado indica llamar en esa fecha
                                                                </td>
                                                                <td>
                                                                    10/05/2017
                                                                </td>
                                                                <td>
                                                                    PENDIENTE
                                                                </td>
                                                                <td>
                                                                    <a class="btn btn-green" id="A2" href="#" data-toggle="modal" data-target="#mdAcuerdo">
                                                                        <i class="ion-android-add"></i>&nbsp;Acuerdo(0)</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="modal-footer">
                                <center>
                                    <button type="button" id="btnGuardar" name="btnGuardar" class="btn btn-primary"">
                                        Guardar</button>
                                    <button type="button" class="btn btn-danger" id="BtnCancelar" name="BtnCancelar"
                                        data-dismiss="modal">
                                        Resetear</button>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- row -->
            <div class="row">
                <div class="modal fade" id="mdAcuerdo" role="dialog" aria-labelledby="myModalLabel"
                    style="z-index: 0" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                </button>
                                <h4 class="modal-title" id="H2">
                                    Registrar Acuerdo</h4>
                            </div>
                            <div class="modal-body">
                                <form id="frmAcuerdo" name="frmAcuerdo" enctype="multipart/form-data" class="form-horizontal"
                                method="post" onsubmit="return false;" action="#">
                                <div class="row">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Detalle:</label>
                                        <div class="col-sm-10">
                                            <textarea id="txtdetalle_acu" name="txtdetalle_acu" style="width: 100%" rows="5"></textarea>
                                            <br />
                                            <div style="text-align: right; font-weight: bold;" id="Contador_Acu">
                                                0 Caracteres</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Fecha:</label>
                                        <div class="col-sm-5" id="date-popup-group">
                                            <div class="input-group date">
                                                <input id="txtFecha_acu" name="txtFecha_acu" class="form-control" style="text-align: right;"
                                                    type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline"></i></span>
                                            </div>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            Hora:</label>
                                        <div class="col-sm-3" id="time-popup-group">
                                            
                                            <div class='input-group date' id='dtpHora'>
                                                <input id="txtHora_acu" name="txtHora_acu" class="form-control" style="text-align: center;" type='text' maxlength="7" />
                                            </div>
                                            
                                        </div>
                                    </div>
                                </div>
                                </form>
                            </div>
                            <div class="modal-footer">
                                <center>
                                    <button type="button" id="btnGuardarAcuerdo" name="btnGuardarAcuerdo" class="btn btn-primary">
                                        Guardar</button>
                                    <button type="button" class="btn btn-danger" id="BtnCancelarAcuerdo" name="BtnCancelarAcuerdo"
                                        data-dismiss="modal">
                                        Cancelar</button>
                                </center>
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
                                <h4 class="modal-title" id="H4">Lista de Teléfonos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
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
                                                                rowspan="1" colspan="1" aria-label="Pertenencia: activate to sort column ascending">
                                                                Pertenencia
                                                            </td>
                                                            <td width="10%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                rowspan="1" colspan="1" aria-label="Vigencia: activate to sort column ascending">
                                                                Vigencia
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
