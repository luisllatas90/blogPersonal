<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitarActivos.aspx.vb" Inherits="administrativo_activofijo_m_Interfaces_SolicitarActivos" %>
<html id="Html1" lang="en" runat="server">
<head id="Head1" runat="server">

<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <title>Traslado de Activo Fijo</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
	<script type="text/javascript" src="../../../assets/js/jquery.js"></script>
	<script type="text/javascript" src="../../../assets/js/bootstrap.min.js"></script>	
	<script type="text/javascript" src='../../../assets/js/noty/jquery.noty.js'></script>
    <script type="text/javascript" src='../../../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../../../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src='../../../assets/js/noty/notifications-custom.js'></script>
    <script type="text/javascript" src='../../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>
    
    <!-- Manejo de tablas -->
    <script type="text/javascript" src='../../../assets/js/jquery.dataTables.min.js'></script>
    <link href="../../../assets/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="../../../assets/js/funcionesDataTable.js?y=1" type="text/javascript"></script>
    
    <!-- Piluku -->        
    <link rel="stylesheet" type="text/css" href="../../../assets/css/bootstrap.min.css"/>
	<link rel="stylesheet" href="../../../assets/css/material.css?x=1"/>		
	<link rel="stylesheet" type="text/css" href="../../../assets/css/style.css?y=4"/>

    <script src="../assets/js/funcionesDataTableAF.js" type="text/javascript"></script>
    
	<!-- activo fijo -->
    <link href="../assets/css/style_af.css" rel="stylesheet" type="text/css" />

    <script src="../../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

</head>

<body>

    <div class="container-fluid">
        
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-piluku">
                
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Solicitar Activos
                        </h3>
                    </div>
                    
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                            
                                <form action="#">
                                    <div class="form-group row">
                                        <label class="col-sm-2 text-right">Artículo</label>
                                        <div class="col-sm-6"><input type="text" class="form-control"/></div>
                                        <div class="col-sm-2"><a href="#" id="btnBuscarTraslado" class="btn btn-green btn-xs" style="width:100%"><i class="ion-search"></i>&nbsp;Buscar</a></div>
                                    </div>
                                </form>
                                
                                <br />
                                <div id='divOcultoTraslado' style="width:80%">
                                    <table class='display dataTable cell-border' id='tbTraslado' width="100%" style="font-size:smaller;margin: 0 10%">
                                        <thead>
                                            <tr>
                                                <th>Descripción</th>
                                                <th>Cantidad</th>
                                                <th>Agregar</th>
                                            </tr>
                                        </thead>
                                        
                                        <tbody>
                                            <tr>
                                                <td>Laptop Core i5</td>
                                                <td><input type="text" class="form-control" maxlength="4" size="4" /></td>
                                                <td><a href="#" class="btn btn-primary btn-xs" style="width:20%">&nbsp;s</a></td>
                                            </tr>
                                        </tbody>
                                        
                                        <tfoot>
                                            <tr>
                                                <th colspan="3"></th>
                                            </tr>
                                        </tfoot>
                                    </table>
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
