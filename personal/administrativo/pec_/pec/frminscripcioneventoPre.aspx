<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frminscripcioneventoPre.aspx.vb" Inherits="administrativo_pec_frminscripcioneventoPre" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/>
    <META HTTP-EQUIV="Pragma" CONTENT="no-cache"/>
    <link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1'/>
    <link rel='stylesheet' href='../../assets/css/material.css'/>
    <link rel='stylesheet' href='../../assets/css/style.css?x=1'/>
    
    
    <script type="text/javascript" src='../../assets/js/jquery.js'></script>
    <script type="text/javascript" src='../../assets/js/app.js'></script>
    <script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>
    <script type="text/javascript" src='../../assets/js/bootstrap.min.js'></script>
    <script type="text/javascript" src='../../assets/js/jquery.nicescroll.min.js'></script>
    <script type="text/javascript" src='../../assets/js/wow.min.js'></script>
    <script type="text/javascript" src="../../assets/js/jquery.nicescroll.min.js"></script>
    <script type="text/javascript" src='../../assets/js/jquery.loadmask.min.js'></script>
    <script type="text/javascript" src='../../assets/js/jquery.accordion.js'></script>
    <script type="text/javascript" src='../../assets/js/materialize.js'></script>
    <script type="text/javascript" src='../../assets/js/bic_calendar.js'></script>
    <script type="text/javascript" src='../../assets/js/core.js'></script>
    <script type="text/javascript" src='../../assets/js/jquery.countTo.js'></script>
    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>
    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>
    <script type="text/javascript" src='../../assets/js/jquery.dataTables.min.js'></script>
    <link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css'/>   
    <script type="text/javascript" src='../../assets/js/funciones.js?z=1'></script>
    <script type="text/javascript" src='../../assets/js/funcionesDataTable.js?z=1'></script>
    
    <script type="text/javascript" src="../../assets/js/DataJson/jsselect.js?z=1"></script>
    <script type="text/javascript" src='../../assets/js/form-elements.js'></script>
    <script type="text/javascript" src='../../assets/js/select2.js'></script>
    <script type="text/javascript" src='../../assets/js/jquery.multi-select.js'></script>
    <script type="text/javascript" src='../../assets/js/bootstrap-colorpicker.js'></script>
    
    <script type="text/javascript" src='../../assets/js/DataJson/JSinscripcioneventoPre.js?a=1'></script>
    <script src='../../assets/js/bootstrap-datepicker.js?x=1'></script>
    <title></title>
    <script type="text/javascript">     

     </script>
     <style>
     .toolbar
     {
         float:left;
         }
     </style>
</head>
   <body class="" >
      <div class="piluku-preloader text-center">
         <!--<div class="progress">
            <div class="indeterminate"></div>
            </div>-->
         <div class="loader">Loading...</div>
      </div>
      <div class="wrapper">
         <div class="content">
            <div class="overlay"></div>
            <div class="main-content">
              <div class="row">
                  <div class="manage_buttons" id="divOpc">
                     <div class="row">
                        <div id="PanelBusqueda" >
                           <div class="col-md-12 col-sm-12 col-lg-12 search">
                              <div class="page_header">
                                 <div class="pull-left">
                                    <i class="icon ti-layout-media-left page_header_icon"></i>
                                    <span class="main-text">Escuela PreUniversitaria</span>
                                 </div>
                                 <div class="buttons-list">
                                    <div class="pull-right-btn">
                                       <input type="hidden" id="hdTF" value="" runat="server" />
                                      
                                    </div>
                                 </div>
                              </div>
                              <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#" method="post" runat="server">
                                 <input type="hidden" id="process" value="" runat="server" />
                                 <input type="hidden" id="hdT" value="" runat="server" />
                                 <div class="row">
                                    <div class="col-md-9 col-sm-12">
                                    <div class="form-group">
                                          <label class="col-sm-12 col-md-4 control-label">Centro de Costos</label>
                                          <div class="col-sm-12 col-md-8" >                                                                                                
                                             <input type="text" id="busPoint" class="form-control" style="border: solid 1px #337ab7;color:#707780; font-weight:bold;">
                                          </div>
                                       </div>
                                      
                                    </div>
                                   <div class="col-md-3 col-sm-12  col-lg-3" >
                           
                                       <a class="btn btn-primary" id="btnListar" href="#" style="width:50%"><i class="ion-android-search"></i>&nbsp;Listar</a>
                                 
                               
                                    </div>
                         
                                    </div>
                                    
                              </form>
                           </div>
                           
                        </div>
                       
                     </div>
                  </div>
               </div>  
               
               <div class="row" id="PanelEvento">
               
               <div class="panel panel-piluku">
               <div class="panel-heading">
						<h3 class="panel-title">
							<span id="tittle">Inscripci&oacute;n</span>
							<span class="panel-options">
								<a href="#" class="panel-refresh">
									<i class="icon ti-reload"></i>
								</a>
								<a href="#" class="panel-minimize">
									<i class="icon ti-angle-up"></i>
								</a>								
							</span>
						</h3>
					</div>
              <div class="panel-body">
              
              <div role="tabpanel">
							<!-- Nav tabs -->
							<ul class="nav nav-tabs piluku-tabs" role="tablist">
								<li role="presentation" class="active" style="width:25%;"><a href="#evento" aria-controls="evento" role="tab" data-toggle="tab">Datos del Evento</a></li>
								<li role="presentation" style="width:25%;"><a href="#preinscrito" aria-controls="preinscrito" role="tab" data-toggle="tab">Preinscritos</a></li>
								<li role="presentation" style="width:25%;"><a href="#inscripciones" aria-controls="inscripciones" role="tab" data-toggle="tab" id="lnkInscripciones" >Inscripciones</a></li>
								<li role="presentation" style="width:25%;"><a href="#inscompleta" aria-controls="inscompleta" role="tab" data-toggle="tab">Inscripci&oacute;n Completa</a></li>
							</ul>

							<!-- Tab panes -->
							<div class="tab-content piluku-tab-content">
								<div role="tabpanel" class="tab-pane active" id="evento">
								<form id="frmEvento" name="frmEvento"   class="form-horizontal"  method="post"  onsubmit="return false;" action="#">
                                     <fieldset>
                                        <legend>Datos Informativos</legend>
                                        <div class="row">                                   
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Nombre Corto</label>
                                                <div class="col-sm-10">
                                                <span id="evenombrecorto" style="font-weight:bold"></span>
                                                </div>
                                            </div>                                        
                                        </div>
                                        <div class="row">                                   
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Nro. Resoluci&oacute;n</label>
                                                <div class="col-sm-10">
                                                <span id="evenroresolucion" style="font-weight:bold"></span>
                                                </div>
                                            </div>                                        
                                        </div>
                                        <div class="row">                                   
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Coordinador Apoyo</label>
                                                <div class="col-sm-10">
                                                <span id="evecordinador" style="font-weight:bold"></span>  
                                                </div>
                                            </div>                                        
                                        </div>
                                        <div class="row">                                   
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Fecha Inicio Propuesta</label>
                                                <div class="col-sm-10">
                                                <span id="evefecini" style="font-weight:bold"></span>
                                                </div>
                                            </div>                                        
                                        </div>
                                        <div class="row">                                   
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Fecha Fin Propuesta</label>
                                                <div class="col-sm-10">
                                                <span id="evefecfin" style="font-weight:bold"></span>
                                                </div>
                                            </div>                                        
                                        </div>
                                    </fieldset>	
                                    <fieldset>
                                        <legend>Precios / Descuentos por participante</legend>
                                        <div class="row">                                   
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Meta de Participantes</label>
                                                <div class="col-sm-10">
                                                <span id="evemeta" style="font-weight:bold"></span>
                                                </div>
                                            </div>                                        
                                        </div>
                                        <div class="row">                                   
                                            <div class="form-group">
                                                <div class="col-sm-6">                                                
                                                <label class="col-sm-4 control-label">Precios</label>
                                                <div class="col-sm-8">                                                
                                                          <div class="row">                                   
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">Contado (S/.)</label>
                                                                <div class="col-sm-7">
                                                                <span id="evecontado" style="font-weight:bold"></span>
                                                                </div>
                                                            </div>                                        
                                                        </div>
                                                          <div class="row">                                   
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">Financiado (S/.)</label>
                                                                <div class="col-sm-7">
                                                                <span id="evefinanciado" style="font-weight:bold"></span>
                                                                </div>
                                                            </div>                                        
                                                        </div>
                                                          <div class="row">                                   
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">Cuota Inicial (S/.)</label>
                                                                <div class="col-sm-7">
                                                                <span id="evemontoincial" style="font-weight:bold"></span>
                                                                </div>
                                                            </div>                                        
                                                        </div>
                                                          <div class="row">                                   
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">Nro de Cuotas (S/.)</label>
                                                                <div class="col-sm-7">
                                                                <span id="evecuota" style="font-weight:bold"></span>
                                                                </div>
                                                            </div>                                        
                                                        </div>
                                                </div>
                                                </div>
                                                <div class="col-sm-6">                                                
                                                <label class="col-sm-4 control-label">% Descuentos</label>
                                                <div class="col-sm-8">                                                
                                                        <div class="row">                                   
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">Personal USAT (%)</label>
                                                                <div class="col-sm-7">
                                                                <span id="evedctoper" style="font-weight:bold"></span>
                                                                </div>
                                                            </div>                                        
                                                        </div>
                                                          <div class="row">                                   
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">Alumno USAT (%)</label>
                                                                <div class="col-sm-7">
                                                                <span id="evedctoalu" style="font-weight:bold"></span>
                                                                </div>
                                                            </div>                                        
                                                        </div>
                                                          <div class="row">                                   
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">Corporativo (%)</label>
                                                                <div class="col-sm-7">
                                                                <span id="evedctocor" style="font-weight:bold"></span>
                                                                </div>
                                                            </div>                                        
                                                        </div>
                                                          <div class="row">                                   
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label">Egresado  (%)</label>
                                                                <div class="col-sm-7">
                                                                <span id="evedctoegr" style="font-weight:bold"></span>
                                                                </div>
                                                            </div>                                        
                                                        </div>
                                                </div>
                                                </div>
                                        </div>
                                    </fieldset>	
                                    <fieldset>
                                        <legend>Otros Datos</legend>
                                        <div class="row">                                   
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Gestiona Notas</label>
                                                <div class="col-sm-10">
                                                <span id="evegestion" style="font-weight:bold"></span>
                                                </div>
                                            </div>                                        
                                        </div>
                                        <div class="row">                                   
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Horarios</label>
                                                <div class="col-sm-10">
                                                <span id="evehorario" style="font-weight:bold"></span>
                                                </div>
                                            </div>                                        
                                        </div>
                                        <div class="row">                                   
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Observaciones</label>
                                                <div class="col-sm-10">
                                                <span id="eveobs" style="font-weight:bold"></span>
                                                </div>
                                            </div>                                        
                                        </div>                                      
                                    </fieldset>	
								</form>
								</div>
								<div role="tabpanel" class="tab-pane" id="preinscrito">Integer ut felis lorem. Sed at venenatis odio. Quisque luctus volutpat sapien, sed rutrum libero congue ut. Maecenas scelerisque sapien ipsum, id iaculis risus aliquet in. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec congue lacinia tristique. Fusce ut aliquam massa.</div>
								<div role="tabpanel" class="tab-pane" id="inscripciones">
								 <div class="row col-md-12"> 
								 
								 <div id="PanelInscripcionListar">
								 <fieldset>							
                                        <legend><i class="ion-android-list"></i>&nbsp;Lista de participantes inscritos con cargo generado</legend>
                                        <div class="row col-md-12">
                                        <form class="form form-horizontal" id="frmBuscarPartInsc" name="frmBuscarPartInsc" onsubmit="return false;" action="#" method="post">
                                        
                                        <div class="btn-group"  >                                        
							                <a href="#" class="btn btn-primary" style="margin-right:2px;"><i class="ion-android-person-add"></i>&nbsp;Persona Natural</a>
							                <a href="#" class="btn btn-primary" style="margin-right:2px;"><i class="ion-android-person-add"></i>&nbsp;persona Jur&iacute;dica</a>
							                <a href="#" class="btn btn-primary" style="margin-right:2px;" id="btnPerNatSinCargo"><i class="ion-android-person-add"></i>&nbsp;Persona Natural Sin Cargo</a>
						                </div>
						                    <select id="cboEstadoParticipanteInsc" name="cboEstadoParticipanteInsc">
							                <option value="T" selected="selected">TODOS</option>
							                <option value="A">ACTIVOS</option>
							                <option value="I">INACTIVOS</option>
							                </select>
							                <a href="#" class="btn btn-primary" id="btnBuscarParticipanteInsc"><i class="ion-search"></i>&nbsp;Buscar</a> 
                                        </form>
                                        </div>                          
                                       <div class="row col-md-12">
                                        <div class="table-responsive">
                                           
                                            <table class="display dataTable" id="dtParticipanteInsc" name="dtParticipanteInsc" width="99%" >
                                            <thead>
                                            <tr>                                            
                                            <th width="4%">&nbsp;</th>
                                            <th width="2%">Nro</th>
                                            <th width="11%">Tipo Doc</th>
                                            <th width="30%">Participante</th>
                                            <th width="4%">Cod. Univ.</th>
                                            <th width="10%">Ciclo Ingreso</th>
                                            <th width="13%">Cargo Total</th>
                                            <th width="13%">Abono Total</th>
                                            <th width="13%">Saldo Total</th>
                                            
                                            </tr>
                                            </thead>
                                            <tbody id="tbParticipanteInsc">                                            
                                            </tbody>
                                            <tfoot>
                                            <tr>                                            
                                            <th colspan="9"></th>                                            
                                            </tr>
                                            </tfoot>
                                            </table>
                                           
                                          </div>
                                      </div>
                                 </fieldset>  
                                 </div> 
                                 <div id="PanelInscripcionRegistroSinCargo" >
                                   <div class="row">
                                    <div class="panel panel-piluku">
                                        <div class="panel-heading" style="background-color:beige;color:Black;font-weight:bold;padding-top:10px;">
                                        <i class="ion-edit "></i>&nbsp;Registro Persona Natural Sin Cargo 
                                        </div>
                                        <div class="panel-body" id="PanelRegPersonasc">
                                        <form class="form form-horizontal" id="frmBuscarPartInscDoc" name="frmBuscarPartInscDoc" onsubmit="return false;" action="#" method="post">
                                        </form>
                                            <form class="form form-horizontal" id="frmPerNatSinCargo" name="frmPerNatSinCargo" onsubmit="return false;" action="#" method="post">
                                                <div class="row"> 
                                                <div class="col-md-12">     
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                        <div class="input-group demo-group" >
                                                        <span class="input-group-addon addon-left">Doc. Identidad</span>
                                                        <select id="cbopscTipoDoc" name="cbopscTipoDoc" class="form-control">
                                                            </select>                                                           
                                                        </div>
                                                            
                                                        </div>                                        
                                                    </div>
                                                    <div class="col-sm-6">
                                                    
                                                        <div class="form-group">
                                                        <div class="input-group demo-group" >
                                                        <span class="input-group-addon addon-left">Nro. Doc. Identidad</span>
                                                        <input type="text" id="txtpscnrodocident" name="txtpscnrodocident" value="" class="form-control" onKeyPress="return soloNumeros(event)" />
                                                                    <span class="input-group-btn">
										                                <button class="btn btn-info btn-xs" type="button" id="btnBuscarPersonasc"><i class="ion-search"></i>&nbsp;Buscar</button>
									                                </span>                                                        
                                                        </div>
                                                        
                                                          
                                                        </div>                                        
                                                    </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                   <div class="col-md-12">
                                                    <div class="form-group">
                                                    <div class="col-md-4">
                                                        <div class="input-group demo-group" >
                                                        <span class="input-group-addon addon-left">Ap.Paterno</span>
                                                        <input type="text" id="txtpscapepat" name="txtpscapepat" value="" class="form-control" />                                                              
                                                        </div>
                                                     </div>
                                                     <div class="col-md-4">
                                                        <div class="input-group demo-group">
                                                        <span class="input-group-addon addon-left">Ap.Materno</span>
                                                        <input type="text" id="txtpscapemat" name="txtpscapemat" value="" class="form-control" />                                                             
                                                        </div>                                                        
                                                     </div>
                                                     <div class="col-md-4">
                                                         <div class="input-group demo-group">
                                                        <span class="input-group-addon addon-left">Nombre</span>
                                                        <input type="text" id="txtpscnombre" name="txtpscnombre" value="" class="form-control" />                                                      
                                                         <span class="input-group-btn">
										                                <button class="btn btn-info btn-xs" type="button" id="btnComprobarNombres"><i class="ion-search"></i>&nbsp;Buscar</button>
									                                </span>  
                                                        </div>     
                                                      </div>
                                                    </div>
                                                   </div>
                                                </div>                                          
                                                
                                                  <div class="row">
                                                    <div class="col-md-12">
                                                    <div class="form-group">
                                                    <div class="col-md-4">
                                                            <div class="picker" style="padding-bottom: 0px;">
					                                            <div class="form-group">
						                                           <%-- <div class="col-md-4">
							                                             <span class="input-group-addon addon-left">Fecha Nac.</span>
						                                            </div>--%>
						                                            <div class="col-md-12" id="date-popup-group">
							                                            <div class="input-group date">
							                                            <span class="input-group-addon addon-left">Fecha Nac.</span>
								                                            <input type="text" class="form-control" id="txtpsfechanac" name="txtpsfechanac" data-provide="datepicker" placeholder="__/__/____" style="text-align:right">
								                                            <span class="input-group-addon sm">
									                                            <i class="ion ion-ios-calendar-outline"></i>
								                                            </span>
							                                            </div>
						                                            </div>
					                                            </div>
				                                            </div> 
                                                    </div>
                                                    <div class="col-md-4">
                                                    <div class="input-group demo-group">
                                                        <span class="input-group-addon addon-left">Sexo</span>
                                                        <select id="cbopscSexo" name="cbopscSexo" class="form-control"></select>                                                     
                                                        </div> 
                                                    </div>
                                                    <div class="col-md-4">
                                                    <div class="input-group demo-group">
                                                        <span class="input-group-addon addon-left">Estado Civil</span>
                                                        <select id="cbopscEstadoCivil" name="cbopscEstadoCivil" class="form-control"></select>                                                    
                                                        </div> 
                                                    </div>
                                                    </div>
                                                    </div>
                                                  </div>                                           
                                                    <div class="row">    
                                                    <div class="col-md-12">  
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                        <div class="input-group demo-group">
                                                        <span class="input-group-addon addon-left">Email Principal</span>
                                                        <input type="text" id="txtpscemailpri" name="txtpscemailpri" value="" class="form-control" />
                                                        </div> 
                                                        
                                                        </div>                                        
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                        <div class="input-group demo-group">
                                                        <span class="input-group-addon addon-left">Email Alternativo</span>
                                                        <input type="text" id="txtpscemailalt" name="txtpscemailalt" value="" class="form-control" />
                                                        </div>                                                            
                                                        </div>                                        
                                                    </div>
                                                    </div>
                                                  </div>
                                                  <div class="row">      
                                                  
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                        <div class="col-md-12">  
                                                            <div class="input-group demo-group">
                                                            <span class="input-group-addon addon-left">Direcci&oacute;n</span>
                                                            <input type="text" id="txtpscdireccion" name="txtpscdireccion" value="" class="form-control" />
                                                            </div>
                                                            </div> 
                                                        </div>                                        
                                                    </div>                                                    
                                                  </div>
                                                  <div class="row">      
                                                    <div class="col-md-12">
                                                     <div class="form-group">
                                                    <div class="col-md-4">                                                   
                                                            <div class="input-group demo-group">
                                                            <span class="input-group-addon addon-left">Departamento</span>
                                                            <select id="cbopscdpto" name="cbopscdpto" class="form-control"></select>
                                                            </div>                                                    
                                                    </div>
                                                    <div class="col-md-4">                                                    
                                                              <div class="input-group demo-group">
                                                              <span class="input-group-addon addon-left">Provincia</span>
                                                              <select id="cbopscdprov" name="cbopscdprov" class="form-control"></select>
                                                              </div>                                                    
                                                    </div>
                                                    <div class="col-md-4">                                                    
                                                              <div class="input-group demo-group">
                                                              <span class="input-group-addon addon-left">Distrito</span>
                                                              <select id="cbopscddist" name="cbopscddist" class="form-control"  ></select>
                                                              </div>                                                    
                                                    </div>
                                                    </div>
                                                    </div>
                                                   </div>
                                                   <div class="row">   
                                                   <div class="col-md-12">     
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                        <div class="input-group demo-group">
                                                              <span class="input-group-addon addon-left">Tel&eacute;fono Fijo</span>
                                                              <input type="text" id="txtpscfono" name="txtpscfono" value="" class="form-control" placeholder="#########" style="text-align:right;" />
                                                              </div>                                                            
                                                        </div>                                        
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                        <div class="input-group demo-group">
                                                              <span class="input-group-addon addon-left">Tel&eacute;fono M&oacute;vil</span>
                                                              <input type="text" id="txtpsccel" name="txtpsccel" value="" class="form-control"  placeholder="#########" style="text-align:right;"/>
                                                              </div>
                                                        </div>                                        
                                                    </div>
                                                    </div>
                                                  </div>
                                                   <div class="row">
                                                      <div class="col-md-12">     
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                        <div class="input-group demo-group">
                                                              <span class="input-group-addon addon-left">Ruc</span>
                                                              <input type="text" id="txtpscruc" name="txtpscruc" value="" class="form-control" placeholder="#########" style="text-align:right;" />
                                                              </div>
                                                        </div>  
                                                        </div>                                        
                                                    
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                        <div class="input-group demo-group">
                                                              <span class="input-group-addon addon-left">Modalidad Ingreso</span>
                                                              <select id="cbopscModIng" name="cbopscModIng" class="form-control"></select>
                                                              </div>                                                            
                                                        </div>                                        
                                                    </div>
                                                    </div>
                                                 </div>
                                                 
                                                
                                                <div class="row col-md-12">  
                                                <div class="form-group"> 
                                                <center>
                                                        <button class="btn btn-primary" id="btnPerNatSinCargoGuardar"><i class="ion ion-checkmark"></i>&nbsp;Guardar</button>
							                            <button class="btn btn-danger" id="btnPerNatSinCargoCancelar"><i class="ion-close-round"></i>&nbsp;Salir&nbsp;</button>							                                                                          
							                    </center>                                                                                                  
							                    </div>
                                                </div>
                                            </form>                                            
                                        </div>
                                    </div>                                 
                                    </div>
                                    
                                    <div class="modal fade modal-full-pad" id="mdListaCoincidencia" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
		                                <div class="modal-dialog modal-full">
                                			<div class="modal-content">
				                                <div class="modal-header" style="background-color:#E33439;" >
					                                <button type="button" class="btn btn-white" data-dismiss="modal" aria-label="Close" style="background-color:red;font-weight:bold;float:right;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					                                <h4 class="modal-title"  style="color:White" >Listado de Personas</h4>
				                                </div>
				                                <div class="modal-body">
                                                    <div class="table-responsive">						                               
                                                               <table class="display cell-border" id="tblPersona" width=100% cellpadding=0 cellspacing=0>
                                                               <thead>                                                              
                                                                   <tr>
                                                                       <th style="width:4%;"></th>
                                                                       <th style="width:15%;">Ap. Paterno</th>
                                                                       <th style="width:15%;">Ap. Materno</th>
                                                                       <th style="width:10%;">Nombres</th>
                                                                       <th style="width:10%;">Doc. Ident</th>
                                                                       <th style="width:10%;">Fec. Nac</th>
                                                                       <th style="width:20%;">Direcci&oacute;n</th>
                                                                       <th style="width:20%;">Email</th>
                                                                   </tr>
                                                               </thead>
                                                               <tbody id="tbdPersona">
                                                               </tbody>
                                                               <tfoot id="tfPersona">
                                                                    <tr>
                                                                        <th colspan="8"></th>
                                                                    </tr>
                                                               </tfoot>
                                                               </table>
                                                     
			                                        </div>			                
					                            </div>
			                                    <div class="modal-footer">
                            				    
				                                </div>
				                            </div>
                            			</div>
		                            </div>                                    
                                 </div>
                                 </div>                                                                   
								</div>
								<div role="tabpanel" class="tab-pane" id="inscompleta">Sed ac fringilla nunc. Fusce scelerisque tempus mauris eu laoreet. Sed sem eros, congue tempor neque non, blandit fringilla nibh. Pellentesque vitae fringilla nibh. Etiam leo elit, faucibus et dignissim quis, auctor vitae lectus. Donec a ex id dolor rhoncus pellentesque. Proin augue urna, commodo id metus in, hendrerit placerat sem.</div>
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
