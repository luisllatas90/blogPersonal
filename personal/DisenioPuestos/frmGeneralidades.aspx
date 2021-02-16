<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGeneralidades.aspx.vb" Inherits="DisenioPuestos_frmGeneralidades" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Registro de Notificaciones</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../Alumni/css/datatables/jquery.dataTables.min.css">      
    <link rel="stylesheet" href="../Alumni/css/sweetalert/sweetalert2.min.css"> 
    <link rel="stylesheet" href="../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../Alumni/css/estilos.css?13">

    <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../Alumni/js/popper.js"></script>    
    <script src="../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../Alumni/js/datatables/jquery.dataTables.min.js?1"></script> 
    <script src="../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    
    <!-- Scripts propios -->
    <script src="../Alumni/js/funciones.js?1"></script>
    
    <script type="text/javascript">
        function udpFiltrosUpdate() {
            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            }); 
        }

        function udpEstructuraUpdate() {
            // Setup - add a text input to each footer cell
            $('#grwEstructura thead tr').clone(true).appendTo( '#grwEstructura thead' );
            $('#grwEstructura thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( table.column(i).search() !== this.value ) {
                        table
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            var table = $('#grwEstructura').DataTable( {
                orderCellsTop: true
            } );
        }

        function udpAreaUpdate() {
            // Setup - add a text input to each footer cell
            $('#grwArea thead tr').clone(true).appendTo( '#grwArea thead' );
            $('#grwArea thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( table.column(i).search() !== this.value ) {
                        table
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            var table = $('#grwArea').DataTable( {
                orderCellsTop: true
            } );
        }

        function udpListaUpdate() {
            formatoGrilla();
        }
        
        function udpRegistroUpdate() { 
            
            
            var date_input=$('input[id="txtFecha1"]'); //our date input has the name "date"
            var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
            var options={
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_input.datepicker(options);


            var date_input=$('input[id="txtFecha2"]'); //our date input has the name "date"
            var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
            var options={
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_input.datepicker(options);


            var date_input=$('input[id="txtFecha3"]'); //our date input has the name "date"
            var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
            var options={
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_input.datepicker(options);
        }

        /* Dar formato a la grilla. */
        function formatoGrilla(){
            // Setup - add a text input to each footer cell
            $('#grwLista thead tr').clone(true).appendTo( '#grwLista thead' );
            $('#grwLista thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( table.column(i).search() !== this.value ) {
                        table
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            var table = $('#grwLista').DataTable( {
                orderCellsTop: true
            } );
        }

        /* Mostrar mensajes al usuario. */
        function showMessage(message, messagetype) {              
            swal({ 
                title: message,
                type: messagetype ,
                confirmButtonText: "OK" ,
                confirmButtonColor: "#45c1e6"
            }).catch(swal.noop);
        } 
        
        function alertConfirm(ctl, event, titulo, icono) {            

            // STORE HREF ATTRIBUTE OF LINK CTL (THIS) BUTTON
            var defaultAction = $(ctl).prop("href");                      
            // CANCEL DEFAULT LINK BEHAVIOUR
            event.preventDefault();            
            
            swal({
                title: titulo,                
                type: icono,
                showCancelButton: true ,
                confirmButtonText: "SI" ,
                confirmButtonColor: "#45c1e6" ,
                cancelButtonText: "NO"
            }).then(function (isConfirm) {
                if (isConfirm) {
                    window.location.href = defaultAction;
                    return true;
                } else {
                    return false;
                }
            }).catch(swal.noop);
        }

        /* Flujo de tabs de la página principal. */
        function flujoTabs(tabActivo) {
            if (tabActivo == 'listado-tab') {
                //HABILITAR
                estadoTabListado('H');                

                //DESHABILITAR
                estadoTabRegistro('D');  
                estadoTabEstructura('D');               
                estadoTabArea('D'); 
            }else if (tabActivo == 'registro-tab'){
                //HABILITAR
                estadoTabRegistro('H');

                //DESHABILITAR
                estadoTabListado('D');
                estadoTabEstructura('D'); 
                estadoTabArea('D'); 
            }else if (tabActivo == 'estructura-tab'){
                //HABILITAR
                estadoTabEstructura('H');

                //DESHABILITAR 
                estadoTabRegistro('D');
                estadoTabListado('D');   
                estadoTabArea('D'); 
            }else if (tabActivo == 'area-tab'){
                //HABILITAR
                estadoTabArea('H');

                //DESHABILITAR 
                estadoTabRegistro('D');
                estadoTabListado('D');   
                estadoTabEstructura('D'); 
            }
        }  

        function estadoTabListado(estado) {
            if (estado == 'H') {
                $("#listado-tab").removeClass("disabled");
                $("#listado-tab").addClass("active");
                $("#listado").addClass("show");
                $("#listado").addClass("active");
            } else {
                $("#listado-tab").removeClass("active");
                $("#listado-tab").addClass("disabled");
                $("#listado").removeClass("show");
                $("#listado").removeClass("active");
            }
        }

        function estadoTabRegistro(estado) {
            if (estado == 'H') {
                $("#registro-tab").removeClass("disabled");
                $("#registro-tab").addClass("active");
                $("#registro").addClass("show");
                $("#registro").addClass("active");
            } else {
                $("#registro-tab").removeClass("active");
                $("#registro-tab").addClass("disabled");
                $("#registro").removeClass("show");
                $("#registro").removeClass("active");
            }
        }

        function estadoTabEstructura(estado) {
            if (estado == 'H') {
                $("#estructura-tab").removeClass("disabled");
                $("#estructura-tab").addClass("active");
                $("#estructura").addClass("show");
                $("#estructura").addClass("active");
            } else {
                $("#estructura-tab").removeClass("active");
                $("#estructura-tab").addClass("disabled");
                $("#estructura").removeClass("show");
                $("#estructura").removeClass("active");
            }
        }  

        function estadoTabArea(estado) {
            if (estado == 'H') {
                $("#area-tab").removeClass("disabled");
                $("#area-tab").addClass("active");
                $("#area").addClass("show");
                $("#area").addClass("active");
            } else {
                $("#area-tab").removeClass("active");
                $("#area-tab").addClass("disabled");
                $("#area").removeClass("show");
                $("#area").removeClass("active");
            }
        }

        function AlternarLoading(retorno, elemento) {
            var $loadingGif ;
            var $elemento ;

            switch (elemento) {
                case 'Lista':
                    $loadingGif = $('.loader');   
                    $elemento = $('#udpLista');               
                    break;

                case 'Registro':                      
                    $loadingGif = $('.loader');                  
                    break;  
                    
                case 'ListaEstructura':
                    $loadingGif = $('#loading-estructura');                    
                    $elemento = $('#udpEstructura');
                    break; 

                case 'ListaArea':
                    $loadingGif = $('#loading-area');                    
                    $elemento = $('#udpArea');
                    break; 

            }

            if ($loadingGif != undefined) {
                if (!retorno) {
                    $loadingGif.fadeIn('slow');  
                    if ($elemento != undefined) {
                        $elemento.addClass("oculto");
                    }                     
                } else {
                    $loadingGif.fadeOut('slow');
                    if ($elemento != undefined) {
                        $elemento.removeClass("oculto");
                    }                    
                }
            } 
        }

        /* Abrir y cerrar el modales. */
        function openModal(elemento) {
            switch (elemento) { 
                case 'RegistrarEstructura':
                    $('#mdlRegistrarEstructura').modal('show');                   
                    break;   
                    
                case 'RegistrarArea':
                    $('#mdlRegistrarArea').modal('show');                   
                    break;     
                                     
            }
        }

        function closeModal(elemento) {
            switch (elemento) { 
                case 'RegistrarEstructura':
                    $('#mdlRegistrarEstructura').modal('hide');                   
                    break;

                case 'RegistrarArea':
                    $('#mdlRegistrarArea').modal('hide');                   
                    break;
                                   
            }            
        }   
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmGeneralidades" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">MANTENIMIENTO DE GENERALIDADES</div>
            </div>
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="listado" aria-selected="true">Listado</a>
                </li>
                <li class="nav-item">
                    <a href="#registro" id="registro-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="registro" aria-selected="false">Registro</a>
                </li>
                <li class="nav-item">
                    <a href="#estructura" id="estructura-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="estructura" aria-selected="false">Estructura Orgánica</a>
                </li>
                <li class="nav-item">
                    <a href="#area" id="area-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="area" aria-selected="false">Área</a>
                </li>
            </ul>
            <div class="tab-content" id="contentTabs">
                <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">Filtros de Búsqueda</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="cmbTipoFiltro" class="col-sm-2 col-form-label form-control-sm">Decreto:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="1">001 – 2021- ASOC</asp:ListItem>
                                                    <asp:ListItem Value="2">002 – 2021- ASOC</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                            </div>
                                        </div>                                        
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-accion btn-celeste">
                                                    <i class="fa fa-sync-alt"></i>
                                                    <span class="text">Listar</span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnNuevo" runat="server" CssClass="btn btn-accion btn-azul"                                                    
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar una nueva generalidad?', 'warning');">
                                                    <i class="fa fa-plus-square"></i>
                                                    <span class="text">Nuevo</span>
                                                </asp:LinkButton>                                                  
                                            </div>
                                        </div>                                        
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_gen"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="decreto" HeaderText="DECRETO" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="version" HeaderText="VERSION" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnGestionarEstructura" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="GestionarEstructura" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Gestionar estructura orgánica"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea gestionar la estructura orgánica?', 'warning');">
                                                    <span><i class="fa fa-user-friends"></i></span>                                                
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnGestionarArea" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="GestionarArea" 
                                                    CssClass="btn btn-warning btn-sm" 
                                                    ToolTip="Gestionar áreas"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea gestionar las áreas?', 'warning');">
                                                    <span><i class="fa fa-user-friends"></i></span>                                                
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar notificación"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Eliminar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Eliminar notificación"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el registro seleccionado?', 'error');">
                                                    <span><i class="fa fa-trash"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                          
                                    </Columns>
                                </asp:GridView>                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br/>
                    </div>
                    <br/>
                </div>
                <div class="tab-pane" id="registro" role="tabpanel" aria-labelledby="registro-tab">                    
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">Datos</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtDecreto" class="col-sm-2 col-form-label form-control-sm">Decreto N°:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtDecreto" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtVersion" class="col-sm-1 col-form-label form-control-sm">Versión:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtVersion" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtElaborado" class="col-sm-2 col-form-label form-control-sm">Elaborado por:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtElaborado" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                              
                                            <label for="txtFecha1" class="col-sm-1 col-form-label form-control-sm">Fecha<span class="requerido">(*)</span>:</label>
                                            <div class="input-group col-sm-2">
                                                <asp:TextBox ID="txtFecha1" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>                                                
                                            </div>                                       
                                        </div>
                                        <div class="row">
                                            <label for="txtRevisado" class="col-sm-2 col-form-label form-control-sm">Revisado por:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtRevisado" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div> 
                                            <label for="txtFecha2" class="col-sm-1 col-form-label form-control-sm">Fecha<span class="requerido">(*)</span>:</label>
                                            <div class="input-group col-sm-2">
                                                <asp:TextBox ID="txtFecha2" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>                                                
                                            </div>                                        
                                        </div> 
                                        <div class="row">
                                            <label for="txtAprobado" class="col-sm-2 col-form-label form-control-sm">Aprobado por:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtAprobado" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div> 
                                            <label for="txtFecha3" class="col-sm-1 col-form-label form-control-sm">Fecha<span class="requerido">(*)</span>:</label>
                                            <div class="input-group col-sm-2">
                                                <asp:TextBox ID="txtFecha3" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>                                                
                                            </div>                                        
                                        </div> 
                                        <div class="row">
                                            <label for="txtPresentacion" class="col-sm-2 col-form-label form-control-sm">Presentación:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtPresentacion" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>
                                        <div class="row"> 
                                            <label for="txtArchivo" class="col-sm-2 col-form-label form-control-sm">Cargar Organigrama:</label> 
                                            <div class="col-sm-7">
                                                <asp:FileUpload ID="txtArchivo" runat="server" CssClass="form-control form-control-sm" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtFinalidad" class="col-sm-2 col-form-label form-control-sm">Finalidad:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtFinalidad" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div> 
                                        <div class="row">
                                            <label for="txtMarco" class="col-sm-2 col-form-label form-control-sm">Marco Legal:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtMarco" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div> 
                                        <div class="row">
                                            <label for="txtAlcance" class="col-sm-2 col-form-label form-control-sm">Alcance:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtAlcance" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div> 
                                        <div class="row">
                                            <label for="txtAprobacion" class="col-sm-2 col-form-label form-control-sm">Aprobación:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtAprobacion" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>                                                                                
                                    </div>
                                    <div class="card-footer" style="text-align: center;">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar los datos generales?', 'warning');">
                                            <i class="fa fa-save"></i>
                                            <span class="text">Guardar</span>
                                        </asp:LinkButton>    
                                        <asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-accion btn-danger">
                                            <i class="fa fa-sign-out-alt"></i>
                                            <span class="text">Salir</span>
                                        </asp:LinkButton>                                                  
                                    </div>                                    
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div> 
                </div>
                <div class="tab-pane" id="estructura" role="tabpanel" aria-labelledby="estructura-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpFiltrosEstructura" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">GESTIÓN DE ESTRUCTURA ORGÁNICA</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:LinkButton ID="btnNuevoEstructura" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar un nuevo item?', 'warning');">
                                                    <i class="fa fa-user-plus"></i>
                                                    <span class="text">Nuevo</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnSalirEstructura" runat="server" CssClass="btn btn-accion btn-rojo">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
                                                </asp:LinkButton> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br/>
                        <div class="table-responsive">
                            <div id="loading-estructura" class="loading oculto">
                                <img src="img/loading.gif">
                            </div> 
                            <asp:UpdatePanel ID="udpEstructura" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwEstructura" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_est"
                                        CssClass="display table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="organo" HeaderText="ÓRGANO" />
                                            <asp:BoundField DataField="nombre" HeaderText="NOMBRE" />                                                                                               
                                            <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditarEstructura" runat="server" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CommandName="Editar" 
                                                        CssClass="btn btn-primary btn-sm" 
                                                        ToolTip="Editar"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                        <span><i class="fa fa-pen"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarEstructura" runat="server" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                        CommandName="Eliminar" 
                                                        CssClass="btn btn-danger btn-sm" 
                                                        ToolTip="Eliminar"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el registro seleccionado?', 'error');">
                                                        <span><i class="fa fa-trash"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br/>
                        </div>
                        <br/>
                    </div>                    
                </div>
                <div class="tab-pane" id="area" role="tabpanel" aria-labelledby="area-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpFiltrosArea" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">GESTIÓN DE ÁREAS</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:LinkButton ID="btnNuevoArea" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar un nuevo item?', 'warning');">
                                                    <i class="fa fa-user-plus"></i>
                                                    <span class="text">Nuevo</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnSalirArea" runat="server" CssClass="btn btn-accion btn-rojo">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
                                                </asp:LinkButton> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br/>
                        <div class="table-responsive">
                            <div id="loading-area" class="loading oculto">
                                <img src="img/loading.gif">
                            </div> 
                            <asp:UpdatePanel ID="udpArea" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwArea" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_est"
                                        CssClass="display table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="area" HeaderText="ÁREA" />                                                                                             
                                            <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditarArea" runat="server" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CommandName="Editar" 
                                                        CssClass="btn btn-primary btn-sm" 
                                                        ToolTip="Editar"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                        <span><i class="fa fa-pen"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarArea" runat="server" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                        CommandName="Eliminar" 
                                                        CssClass="btn btn-danger btn-sm" 
                                                        ToolTip="Eliminar"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el registro seleccionado?', 'error');">
                                                        <span><i class="fa fa-trash"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br/>
                        </div>
                        <br/>
                    </div>                    
                </div>
            </div>                        
        </div>
        <!-- Modal Registro de Estructura -->
        <div id="mdlRegistrarEstructura" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpRegistroEstructura" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">           
                                <span class="modal-title">ITEM</span>             
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>                                                
                            </div>
                            <div class="modal-body">
                                <div class="panel-cabecera">
                                    <div class="card">
                                        <div class="card-header">Registro de item</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="cmbOrgano" class="col-sm-2 col-form-label form-control-sm">Órgano:</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="cmbOrgano" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                        <asp:ListItem Value="1">Órganos de dirección</asp:ListItem>
                                                        <asp:ListItem Value="2">Órganos de asesoría</asp:ListItem>     
                                                        <asp:ListItem Value="3">Órganos de apoyo</asp:ListItem> 
                                                        <asp:ListItem Value="4">Órganos de línea</asp:ListItem>                                             
                                                    </asp:DropDownList>
                                                </div>
                                            </div>  
                                            <div class="row">
                                                <label for="txtNombres" class="col-sm-2 col-form-label form-control-sm">Nombre:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtNombres" runat="server" MaxLength="150" CssClass="form-control form-control-sm uppercase" 
                                                        onkeypress="javascript:return soloLetras(event)" AutoComplete="off"/>
                                                </div>
                                            </div>                                             
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">                                                                
                                <asp:LinkButton ID="btnGuardarEstructura" runat="server" CssClass="btn btn-accion btn-verde"
                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar la información?', 'warning');">
                                    <i class="fa fa-save"></i>
                                    <span class="text">Guardar</span>
                                </asp:LinkButton>  
                                <button type="button" id="btnCerrarModal" class="btn btn-danger" data-dismiss="modal">
                                    <i class="fa fa-sign-out-alt"></i>
                                    <span class="text">Salir</span>
                                </button>                                                                                         
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- Modal Registro de Area -->
        <div id="mdlRegistrarArea" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpRegistroArea" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">           
                                <span class="modal-title">ITEM</span>             
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>                                                
                            </div>
                            <div class="modal-body">
                                <div class="panel-cabecera">
                                    <div class="card">
                                        <div class="card-header">Registro de item</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="cmbArea" class="col-sm-2 col-form-label form-control-sm">Área:</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="cmbArea" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                        <asp:ListItem Value="1">Facultad de Medicina</asp:ListItem>
                                                        <asp:ListItem Value="2">Facultad de Ciencias Empresariales</asp:ListItem>     
                                                        <asp:ListItem Value="3">Facultad de Derecho</asp:ListItem> 
                                                        <asp:ListItem Value="4">Dirección Escuela de Economía</asp:ListItem>                                             
                                                    </asp:DropDownList>
                                                </div>
                                            </div>  
                                            <div class="row">
                                                <label for="txtFunciones" class="col-sm-2 col-form-label form-control-sm">Funciones Generales:</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtFunciones" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                                </div> 
                                            </div>
                                            <div class="row"> 
                                                <label for="txtArchivo1" class="col-sm-2 col-form-label form-control-sm">Cargar Diagrama Jerárquica:</label> 
                                                <div class="col-sm-7">
                                                    <asp:FileUpload ID="txtArchivo1" runat="server" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="row"> 
                                                <label for="txtArchivo2" class="col-sm-2 col-form-label form-control-sm">Cargar Organigrama:</label> 
                                                <div class="col-sm-7">
                                                    <asp:FileUpload ID="txtArchivo2" runat="server" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>                                             
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">                                                                
                                <asp:LinkButton ID="btnGuardarArea" runat="server" CssClass="btn btn-accion btn-verde"
                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar la información?', 'warning');">
                                    <i class="fa fa-save"></i>
                                    <span class="text">Guardar</span>
                                </asp:LinkButton>  
                                <button type="button" id="btnCerrarModal" class="btn btn-danger" data-dismiss="modal">
                                    <i class="fa fa-sign-out-alt"></i>
                                    <span class="text">Salir</span>
                                </button>                                                                                         
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        var controlId = '';

        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {   
            udpFiltrosUpdate();
            udpRegistroUpdate();            

            /*Ocultar cargando*/   
            $('.loader').fadeOut("slow");
        });

        /* Mostrar y ocultar gif al realizar un procesamiento. */
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            switch (controlId) {                
                case 'btnListar':
                    AlternarLoading(false, 'Lista');                      
                    break; 
                
                case 'btnGuardar':
                    AlternarLoading(false, 'Registro');
                                    
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var error = args.get_error();
            if (error) {                
                AlternarLoading(true, '');               
                return false;
            }

            switch (controlId) {                                
                case 'btnListar':
                    AlternarLoading(true, 'Lista');                                    
                    break;   

                case 'btnGuardar':
                    AlternarLoading(true, 'Registro');
                            
            }
        });    
    </script>
</body>
</html>
