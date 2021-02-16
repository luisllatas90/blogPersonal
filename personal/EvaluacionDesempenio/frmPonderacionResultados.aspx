<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPonderacionResultados.aspx.vb" Inherits="EvaluacionDesempenio_frmPonderacionResultados" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />      
    <title>Ponderación para la Evaluación de Resultados</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../Alumni/css/datatables/jquery.dataTables.min.css">      
    <link rel="stylesheet" href="../Alumni/css/sweetalert/sweetalert2.min.css">     
    
    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../Alumni/css/estilos.css">
    
    <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../Alumni/js/popper.js"></script>    
    <script src="../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../Alumni/js/datatables/jquery.dataTables.min.js?1"></script> 
    
    <!-- Scripts propios -->
    <script src="../Alumni/js/funciones.js?1"></script>
    
    <script type="text/javascript">
        function udpFiltrosUpdate() {
            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            }); 
        }

        function udpListaUpdate() {
            formatoGrilla();
        }

        function udpRegistroUpdate() { 
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
        
            $('#grwLista').DataTable({
                orderCellsTop: true
            });
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
                estadoTabResponsables('D');                

            }else if (tabActivo == 'responsables-tab'){
                //HABILITAR
                estadoTabResponsables('H');

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

        function estadoTabResponsables(estado) {
            if (estado == 'H') {
                $("#responsables-tab").removeClass("disabled");
                $("#responsables-tab").addClass("active");
                $("#responsables").addClass("show");
                $("#responsables").addClass("active");
            } else {
                $("#responsables-tab").removeClass("active");
                $("#responsables-tab").addClass("disabled");
                $("#responsables").removeClass("show");
                $("#responsables").removeClass("active");
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

        /* Abrir y cerrar el modales. */
        function openModal(elemento) {
            $('#mdl'+elemento).modal('show');                                       
        }

        function closeModal(elemento) {   
            $('#mdl'+elemento).modal('hide');        
        }        
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmPonderacionResultados" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">PONDERACIÓN PARA LA EVALUACIÓN DE RESULTADOS</div>
            </div>
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="listado" aria-selected="true">Listado</a>
                </li>
                <li class="nav-item">
                    <a href="#responsables" id="responsables-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="responsables" aria-selected="false">Responsables</a>
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
                                            <label for="cmbTipoTrabajadorFiltro" class="col-sm-2 col-form-label form-control-sm">Tipo Trabajador:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoTrabajadorFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="1">DOCENTE</asp:ListItem>
                                                    <asp:ListItem Value="2">JEFE DE PRÁCTICAS</asp:ListItem>
                                                    <asp:ListItem Value="3">ADMINISTRATIVO</asp:ListItem>
                                                    <asp:ListItem Value="4">DE SERVICIO</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-accion btn-celeste">
                                                    <i class="fa fa-sync-alt"></i>
                                                    <span class="text">Actualizar</span>
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
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames=""
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="tipo_evaluacion" HeaderText="TIPO DE EVALUACIÓN" ItemStyle-Width="35%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="peso" HeaderText="PESO" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar evaluación"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Eliminar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Eliminar evaluación"
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
                <div class="tab-pane" id="responsables" role="tabpanel" aria-labelledby="responsables-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpListaResponsables" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">RESPONSABLES DE LA EVALUACIÓN</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:LinkButton ID="btnNuevoResponsable" runat="server" CssClass="btn btn-accion btn-verde">
                                                    <i class="fa fa-plus-circle"></i>
                                                    <span class="text">Nuevo</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnSalirResponsable" runat="server" CssClass="btn btn-accion btn-rojo">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
                                                </asp:LinkButton> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br/>
                                <div class="table-responsive">
                                    <asp:GridView ID="grwListaResponsables" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames=""
                                        CssClass="display table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="reponsable" HeaderText="RESPONSABLE" ItemStyle-Width="35%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="peso" HeaderText="PESO" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                            <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CommandName="Editar" 
                                                        CssClass="btn btn-primary btn-sm" 
                                                        ToolTip="Editar evaluación"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                        <span><i class="fa fa-pen"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminar" runat="server" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                        CommandName="Eliminar" 
                                                        CssClass="btn btn-danger btn-sm" 
                                                        ToolTip="Eliminar evaluación"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el registro seleccionado?', 'error');">
                                                        <span><i class="fa fa-trash"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br/>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Registro -->
        <div id="mdlRegistro" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">REGISTRO DE PONDERACIÓN</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="cmbTipoTrabajador" class="col-sm-2 col-form-label form-control-sm">Tipo Trabajador:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoTrabajador" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="1">DOCENTE</asp:ListItem>
                                                    <asp:ListItem Value="2">JEFE DE PRÁCTICAS</asp:ListItem>
                                                    <asp:ListItem Value="3">ADMINISTRATIVO</asp:ListItem>
                                                    <asp:ListItem Value="4">DE SERVICIO</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label for="cmbTipoEvaluacion" class="col-sm-2 col-form-label form-control-sm">Tipo Evaluación:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoEvaluacion" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="1">COMPETENCIAS GENERALES</asp:ListItem>
                                                    <asp:ListItem Value="2">COMPETENCIAS ESPECÍFICAS</asp:ListItem>
                                                    <asp:ListItem Value="3">COMPROMISO INSTITUCIONAL</asp:ListItem>                                                    
                                                </asp:DropDownList>
                                            </div>
                                        </div>
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
