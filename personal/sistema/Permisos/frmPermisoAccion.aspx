﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPermisoAccion.aspx.vb" Inherits="sistema_Permisos_frmPermisoAccion" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Registro de Permisos</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../../Alumni/css/datatables/jquery.dataTables.min.css">      
    <link rel="stylesheet" href="../../Alumni/css/sweetalert/sweetalert2.min.css"> 

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../../Alumni/css/estilos.css?1">

    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../../Alumni/js/popper.js"></script>    
    <script src="../../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../../Alumni/js/datatables/jquery.dataTables.min.js?1"></script> 
    
    <!-- Scripts propios -->
    <script src="../../Alumni/js/funciones.js?1"></script>
    
    <script type="text/javascript">
    
        function udpRegistroUpdate(){
            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });              
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

            }else if (tabActivo == 'registro-tab'){
                //HABILITAR
                estadoTabRegistro('H');

                //DESHABILITAR
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

    </script>

</head>
<body>
    <div class="loader"></div>

    <form id="frmPermisoAccion" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">REGISTRO DE PERMISOS</div>
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
                                            <label for="txtNombreFiltro" class="col-sm-2 col-form-label form-control-sm">Título del Formulario:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtNombreFiltro" runat="server" MaxLength="500" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
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
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar un nuevo permiso?', 'warning');">
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
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_pea"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>                                        
                                        <asp:BoundField DataField="formulario_pea" HeaderText="FORMULARIO" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="nombreFormulario_pea" HeaderText="TÍTULO" ItemStyle-Width="20%" ItemStyle-Wrap="false" />                                                                                
                                        <asp:BoundField DataField="accion_pea" HeaderText="ACCIÓN" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="descripcion_pea" HeaderText="DESCRIPCIÓN" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar permiso"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Eliminar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Eliminar permiso"
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
                                    <div class="card-header">Datos del Permiso</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtFormulario" class="col-sm-2 col-form-label form-control-sm">Formulario<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtFormulario" runat="server" MaxLength="500" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                             
                                        </div> 
                                        <div class="row">
                                            <label for="txtNombre" class="col-sm-2 col-form-label form-control-sm">Título<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtNombre" runat="server" MaxLength="500" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                             
                                        </div>                                                                               
                                        <div class="row">
                                            <label for="txtAccion" class="col-sm-2 col-form-label form-control-sm">Acción<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbAccion" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="AGREGAR">AGREGAR</asp:ListItem>
                                                    <asp:ListItem Value="EDITAR">EDITAR</asp:ListItem>
                                                    <asp:ListItem Value="ELIMINAR">ELIMINAR</asp:ListItem>
                                                    <asp:ListItem Value="CONSULTAR">CONSULTAR</asp:ListItem>                                                    
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtAccion" runat="server" MaxLength="150" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                             
                                        </div>
                                        <div class="row">
                                            <label for="txtDescripcion" class="col-sm-2 col-form-label form-control-sm">Descripción:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="500" CssClass="form-control form-control-sm uppercase" TextMode="MultiLine" Rows="3" AutoComplete="off"/>
                                            </div> 
                                        </div>
                                        <div class="row">
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>                                            
                                        </div>                                         
                                    </div>
                                    <div class="card-footer" style="text-align: center;">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar el permiso?', 'warning');">
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
            </div>
        </div>
    </form>

    <script type="text/javascript">
        var controlId = '';

        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {               
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
