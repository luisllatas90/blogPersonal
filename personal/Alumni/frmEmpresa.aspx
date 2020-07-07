<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmpresa.aspx.vb" Inherits="Alumni_frmEmpresa" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Empresas</title>

    <!-- Estilos externos -->         
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="css/datatables/jquery.dataTables.min.css">   
    <link rel="stylesheet" href="css/sweetalert/sweetalert2.min.css">

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="css/estilos.css?4">

    <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>        
    <script src="js/popper.js"></script>    
    <script src="js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="js/sweetalert/sweetalert2.js"></script>
    <script src="js/datatables/jquery.dataTables.min.js?1"></script> 

    <!-- Scripts propios -->
    <script src="js/funciones.js?2"></script>    

    <script type="text/javascript">

        /* Scripts a ejecutar al actualizar los paneles.*/
        function udpFiltrosUpdate() {
            $('#cmbEstadoFiltro').selectpicker({
                size: 6,
            });
        }

        function udpRegistroUpdate() {
            $('#cmbTipoEmpresa').selectpicker({
                size: 6,
            });

            $('#cmbSector').selectpicker({
                size: 6,
            });

            $('#cmbDepartamento').selectpicker({
                size: 6,
            });

            $('#cmbProvincia').selectpicker({
                size: 6,
            });

            $('#cmbDistrito').selectpicker({
                size: 6,
            });

            $('#cmbEstado').selectpicker({
                size: 6,
            });

            $('#cmbTelefono').selectpicker({
                size: 6,
            });
        }

        function udpRegistroContactoUpdate() {
            $('#cmbDenominacionContacto').selectpicker({
                size: 6,
            });

            $('#cmbTelefonoContacto').selectpicker({
                size: 6,
            });
        }

        function udpContactosUpdate() {
            // Setup - add a text input to each footer cell
            $('#grwContactos thead tr').clone(true).appendTo( '#grwContactos thead' );
            $('#grwContactos thead tr:eq(1) th').each( function (i) {
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
        
            var table = $('#grwContactos').DataTable( {
                orderCellsTop: true
            } );
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
                    if (ctl.id == "btnGuardar"){
                        AlternarLoading(false, 'Registro');                        
                    }

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
                estadoTabContactos('D');                   

            }else if (tabActivo == 'registro-tab'){
                //HABILITAR             
                estadoTabRegistro('H');

                //DESHABILITAR 
                estadoTabListado('D');                
                estadoTabContactos('D');

            }else if (tabActivo == 'contactos-tab'){
                //HABILITAR
                estadoTabContactos('H');

                //DESHABILITAR 
                estadoTabRegistro('D');
                estadoTabListado('D');   
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

        function estadoTabContactos(estado) {
            if (estado == 'H') {
                $("#contactos-tab").removeClass("disabled");
                $("#contactos-tab").addClass("active");
                $("#contactos").addClass("show");
                $("#contactos").addClass("active");
            } else {
                $("#contactos-tab").removeClass("active");
                $("#contactos-tab").addClass("disabled");
                $("#contactos").removeClass("show");
                $("#contactos").removeClass("active");
            }
        }    

        function AlternarLoading(retorno, elemento) {
            var $loadingGif ;
            var $elemento ;

            switch (elemento) { 
                case 'Lista':
                    $loadingGif = $('#loading-lista');                    
                    $elemento = $('#udpLista');
                    break;
                case 'Registro':                      
                    $loadingGif = $('.loader');                  
                    break;     
                case 'BuscarRUC':                    
                    $loadingGif = $('.loader');                   
                    break; 
                case 'ListaContactos':
                    $loadingGif = $('#loading-contactos');                    
                    $elemento = $('#udpContactos');
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
                case 'RegistrarContacto':
                    $('#mdlRegistrarContacto').modal('show');                   
                    break;    
                
                case 'AccesoCampus':
                    $('#mdlAccesoCampus').modal('show');                   
                    break;  

                case 'Logo':
                    $('#mdlLogo').modal('show');                   
                    break;                       
            }
        }

        function closeModal(elemento) {
            switch (elemento) { 
                case 'RegistrarContacto':
                    $('#mdlRegistrarContacto').modal('hide');                   
                    break;
                
                case 'AccesoCampus':
                    $('#mdlAccesoCampus').modal('hide');                   
                    break;

                case 'Logo':
                    $('#mdlLogo').modal('hide');                   
                    break;                    
            }            
        }          
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmEmpresa" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">GESTIÓN DE EMPRESAS</div>
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
                    <a href="#contactos" id="contactos-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="contactos" aria-selected="false">Contactos</a>
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
                                            <label for="txtNombreFiltro" class="col-sm-2 form-control-sm">Nombre / RUC:</label>
                                            <div class="col-sm-5">                                                                                
                                                <asp:TextBox ID="txtNombreFiltro" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="cmbEstadoFiltro" class="col-sm-1 col-form-label form-control-sm">Estado:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbEstadoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-accion btn-celeste">
                                                    <i class="fa fa-sync-alt"></i>
                                                    <span class="text">Listar</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnNuevo" runat="server" CssClass="btn btn-accion btn-azul">
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
                    <br/>
                    <div class="table-responsive">
                        <div id="loading-lista" class="loading oculto">
                            <img src="img/loading.gif">
                        </div>
                        <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_emp, idPro"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="nombreComercial_emp" HeaderText="NOMBRE COMERCIAL" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="ruc_emp" HeaderText="RUC" />
                                        <asp:BoundField DataField="telefono_completo" HeaderText="TELÉFONO" />
                                        <asp:BoundField DataField="celular_emp" HeaderText="CELULAR" />
                                        <asp:BoundField DataField="correo_emp" HeaderText="CORREO" />
                                        <asp:BoundField DataField="acceso_campus" HeaderText="CAMPUS" />
                                        <asp:BoundField DataField="estado_cat" HeaderText="ESTADO" />
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar empresa"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnGestionarContacto" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="GestionarContacto" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Gestionar contactos"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea gestionar los contactos de la empresa?', 'warning');">
                                                    <span><i class="fa fa-user-friends"></i></span>                                                
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnAccesoCampus" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="AccesoCampus" 
                                                    CssClass="btn btn-info btn-sm" 
                                                    ToolTip="Enviar acceso a campus"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea enviar el acceso a campus empresa?', 'warning');">
                                                    <span><i class="fa fa-sign-in-alt"></i></span>                                                
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
                                    <div class="card-header">Información de la Empresa</div>
                                    <div class="card-body">                                      
                                        <div class="row">
                                            <label for="txtRuc" class="col-sm-2 col-form-label form-control-sm">RUC<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtRuc" runat="server" MaxLength="11" CssClass="form-control form-control-sm"
                                                    onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:LinkButton ID="btnBuscarEmpresa" runat="server" CssClass="btn btn-accion btn-azul">
                                                    <i class="fa fa-search"></i>
                                                    <span class="text">Buscar</span>
                                                </asp:LinkButton>
                                            </div>
                                            <label for="cmbEstado" class="col-sm-1 col-form-label form-control-sm">Estado:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbEstado" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" Enabled="False" AutoComplete="off"/>                                    
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtNombreComercial" class="col-sm-2 col-form-label form-control-sm">Nombre Comercial<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtNombreComercial" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>                                        
                                        <div class="row">
                                            <label for="txtRazonSocial" class="col-sm-2 col-form-label form-control-sm">Razón Social:</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtRazonSocial" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtAbreviatura" class="col-sm-2 col-form-label form-control-sm">Abreviatura:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtAbreviatura" runat="server" MaxLength="15" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="cmbTipoEmpresa" class="col-sm-2 col-form-label form-control-sm">Tipo:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbTipoEmpresa" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbSector" class="col-sm-1 col-form-label form-control-sm">Sector:</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="cmbSector" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div> 
                                        </div>
                                        <div class="row">
                                            <label for="cmbDepartamento" class="col-sm-2 col-form-label form-control-sm">Departamento:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbDepartamento" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbProvincia" class="col-sm-1 col-form-label form-control-sm">Provincia:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbProvincia" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbDistrito" class="col-sm-1 col-form-label form-control-sm">Distrito:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbDistrito" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtDireccion" class="col-sm-2 col-form-label form-control-sm">Dirección:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtDireccion" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtDireccionWeb" class="col-sm-1 col-form-label form-control-sm">Página Web:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtDireccionWeb" runat="server" MaxLength="300" CssClass="form-control form-control-sm" Placeholder="http://www.miempresa.com" AutoComplete="off"/>
                                            </div>              
                                        </div>
                                        <div class="row">
                                            <label for="txtCorreo" class="col-sm-2 col-form-label form-control-sm">Correo:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtCorreo" runat="server" MaxLength="100" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                            </div> 
                                            <label for="cmbTelefono" class="col-sm-1 col-form-label form-control-sm">Teléfono:</label>
                                            <div class="col-sm-4">
                                                <div class="row">                                                    
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="cmbTelefono" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtTelefono" runat="server" MaxLength="7" CssClass="form-control form-control-sm"
                                                            onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">       
                                            <label for="txtCelular" class="col-sm-2 col-form-label form-control-sm">Celular:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtCelular" runat="server" MaxLength="9" CssClass="form-control form-control-sm"
                                                    onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                            </div>   
                                            <label for="txtLogo" class="col-sm-2 col-form-label form-control-sm">Logo:</label> 
                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtLogo" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btnLogo" runat="server" CssClass="btn btn-accion btn-azul">
                                                            <i class="fa fa-image"></i>
                                                            <span class="text">Ver</span>
                                                        </asp:LinkButton>                                                          
                                                    </div>
                                                </div>
                                            </div>                                                                                    
                                        </div>
                                        <div class="row"> 
                                            <label for="txtArchivo" class="col-sm-2 col-form-label form-control-sm">Cargar Logo:</label> 
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="txtArchivo" runat="server" CssClass="form-control form-control-sm" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="chkAccesoCampus" class="col-sm-2 col-form-label form-control-sm">Acceso Campus:</label>                                   
                                            <div class="col-sm-1">                                                
                                                <asp:CheckBox ID="chkAccesoCampus" AutoPostBack="True" runat="server"/>
                                            </div> 
                                        </div>
                                        <div class="row">
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                        </div>                                         
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: center;">
                                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar la información de la empresa?', 'warning');"
                                                    OnClick="btnGuardar_Click">
                                                    <i class="fa fa-save"></i>
                                                    <span class="text">Guardar</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-accion btn-rojo">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
                                                </asp:LinkButton>                                                                                              
                                            </div>                                 
                                        </div>
                                    </div> 
                                </div>                               
                                <br/>
                            </ContentTemplate>

                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnGuardar" />
                            </Triggers>                             
                        </asp:UpdatePanel>                                            
                    </div>
                </div>
                <div class="tab-pane" id="contactos" role="tabpanel" aria-labelledby="contactos-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpFiltrosContactos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">GESTIÓN DE CONTACTOS</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:LinkButton ID="btnNuevoContacto" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar un nuevo contacto?', 'warning');">
                                                    <i class="fa fa-user-plus"></i>
                                                    <span class="text">Nuevo</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnSalirContacto" runat="server" CssClass="btn btn-accion btn-rojo">
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
                            <div id="loading-contactos" class="loading oculto">
                                <img src="img/loading.gif">
                            </div> 
                            <asp:UpdatePanel ID="udpContactos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwContactos" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_inc"
                                        CssClass="display table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_completo" HeaderText="NOMBRE" />
                                            <asp:BoundField DataField="cargo_inc" HeaderText="CARGO" />
                                            <asp:BoundField DataField="telefono_completo" HeaderText="TELÉFONO" />
                                            <asp:BoundField DataField="celular_inc" HeaderText="CELULAR" />
                                            <asp:BoundField DataField="correo01_inc" HeaderText="CORREO" />                                                    
                                            <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditarContacto" runat="server" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CommandName="Editar" 
                                                        CssClass="btn btn-primary btn-sm" 
                                                        ToolTip="Editar contacto"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                        <span><i class="fa fa-pen"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminarContacto" runat="server" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                        CommandName="Eliminar" 
                                                        CssClass="btn btn-danger btn-sm" 
                                                        ToolTip="Eliminar contacto"
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

        <!-- Modal Registro de Contactos -->
        <div id="mdlRegistrarContacto" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpRegistroContacto" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">           
                                <span class="modal-title">CONTACTOS</span>             
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>                                                
                            </div>
                            <div class="modal-body">
                                <div class="panel-cabecera">
                                    <div class="card">
                                        <div class="card-header">Registro de Contactos</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="cmbDenominacionContacto" class="col-sm-2 col-form-label form-control-sm">Denominación<span class="requerido">(*)</span>:</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="cmbDenominacionContacto" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label for="txtNombresContacto" class="col-sm-2 col-form-label form-control-sm">Nombres<span class="requerido">(*)</span>:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtNombresContacto" runat="server" MaxLength="150" CssClass="form-control form-control-sm uppercase" 
                                                        onkeypress="javascript:return soloLetras(event)" AutoComplete="off"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label for="txtApellidosContacto" class="col-sm-2 col-form-label form-control-sm">Apellidos<span class="requerido">(*)</span>:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtApellidosContacto" runat="server" MaxLength="150" CssClass="form-control form-control-sm uppercase" 
                                                        onkeypress="javascript:return soloLetras(event)" AutoComplete="off"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label for="txtCargoContacto" class="col-sm-2 col-form-label form-control-sm">Cargo:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtCargoContacto" runat="server" MaxLength="150" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label for="txtCorreoContacto1" class="col-sm-2 col-form-label form-control-sm">Correo 1:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtCorreoContacto1" runat="server" MaxLength="150" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label for="txtCorreoContacto2" class="col-sm-2 col-form-label form-control-sm">Correo 2:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtCorreoContacto2" runat="server" MaxLength="150" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                                </div>
                                            </div> 
                                            <div class="row">
                                                <label for="cmbTelefonoContacto" class="col-sm-1 offset-sm-1 col-form-label form-control-sm">Teléfono:</label>
                                                <div class="col-sm-5">
                                                    <div class="row">                                                    
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="cmbTelefonoContacto" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtTelefonoContacto" runat="server" MaxLength="7" CssClass="form-control form-control-sm"
                                                                onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>   
                                            <div class="row">
                                                <label for="txtCelularContacto" class="col-sm-2 col-form-label form-control-sm">Celular:</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtCelularContacto" runat="server" MaxLength="9" CssClass="form-control form-control-sm"
                                                        onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                </div>
                                            </div>  
                                            <div class="row">
                                                <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                            </div>                                             
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">                                                                
                                <asp:LinkButton ID="btnGuardarContacto" runat="server" CssClass="btn btn-accion btn-verde"
                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar la información del contacto?', 'warning');">
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

        <!-- Modal Gestionar Accesos a Campus -->
        <div id="mdlAccesoCampus" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpAccesoCampus" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">           
                                <span class="modal-title">ACCESO A CAMPUS</span>             
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>                                                
                            </div>
                            <div class="modal-body">
                                <div class="panel-cabecera">
                                    <div class="card">
                                        <div class="card-header">Enviar Acceso a Empresa</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="txtRucAcceso" class="col-sm-2 col-form-label form-control-sm">RUC:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtRucAcceso" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                                </div>                                                
                                            </div>
                                            <div class="row">
                                                <label for="txtNombreComercialAcceso" class="col-sm-2 col-form-label form-control-sm">Nombre Comercial:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtNombreComercialAcceso" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                                </div>
                                            </div>  
                                            <div class="row">
                                                <label for="txtCorreoAcceso" class="col-sm-2 col-form-label form-control-sm">Correo<span class="requerido">(*)</span>:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtCorreoAcceso" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                                </div> 
                                            </div>
                                            <div class="row">
                                                <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                            </div>                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">                                                                
                                <asp:LinkButton ID="btnEnviarAcceso" runat="server" CssClass="btn btn-accion btn-verde"
                                    OnClientClick="return alertConfirm(this, event, '¿Desea enviar datos de acceso a campus empresa?', 'warning');">
                                    <i class="fa fa-share-square"></i>
                                    <span class="text">Enviar</span>
                                </asp:LinkButton>  
                                <button type="button" id="btnCerrarAcceso" class="btn btn-danger" data-dismiss="modal">
                                    <i class="fa fa-sign-out-alt"></i>
                                    <span class="text">Salir</span>
                                </button>                                                                                         
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <!-- Modal Visualizar Logo -->
        <div id="mdlLogo" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpLogo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">           
                                <span class="modal-title">LOGO</span>             
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>                                                
                            </div> 
                            <div class="modal-body">
                                <div class="panel-cabecera">
                                    <div class="card" style="align-items: center;">
                                        <asp:Image ID="imgLogo" runat="server" ImageUrl="" Width="320px" Height="320px" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">                                                                
                                <button type="button" id="btnCerrarLogo" class="btn btn-danger" data-dismiss="modal">
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
            udpRegistroContactoUpdate();     

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
                case 'btnBuscarEmpresa':   
                    AlternarLoading(false, 'BuscarRUC');
                    break;
                case 'btnGuardarContacto':
                    AlternarLoading(false, 'ListaContactos');
                    break;
                case 'btnEnviarAcceso':
                    AlternarLoading(false, 'Lista');                      
                    break;
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
                case 'btnBuscarEmpresa':   
                    AlternarLoading(true, 'BuscarRUC');
                    break;     
                case 'btnGuardarContacto':
                    AlternarLoading(true, 'ListaContactos');
                    break;   
                case 'btnEnviarAcceso':
                    AlternarLoading(true, 'Lista');                      
                    break;             
            }
        });
    </script>
</body>
</html>
