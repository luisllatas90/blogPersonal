<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGestionRecursoVirtual.aspx.vb" Inherits="Biblioteca_frmGestionRecursoVirtual" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />  
    <title>Gestión de Recursos Virtuales</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../Alumni/css/datatables/jquery.dataTables.min.css">      
    <link rel="stylesheet" href="../Alumni/css/sweetalert/sweetalert2.min.css">  
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote-bs4.css">   

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../Alumni/css/estilos.css">

    <style>
        .div-readonly {
            pointer-events: none;
        }
    </style>

    <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../Alumni/js/popper.js"></script>    
    <script src="../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../Alumni/js/datatables/jquery.dataTables.min.js?1"></script>     
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote-bs4.js"></script>
    
    <!-- Scripts propios -->
    <script src="../Alumni/js/funciones.js"></script>

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

        function udpRegistroDetalleUpdate() { 
            actualizarSummernote();

            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });        
        }

        function actualizarSummernote(){            
            /*Editor de Texto*/
            $('.summernote').summernote('destroy');            
            $('.summernote').summernote({
                placeholder: 'Redacta un texto detalle...',
                tabsize: 2,
                height: 300,
                focus: true
            });   
        }

        function udpListaDetalleUpdate() {            
            formatoGrillaDetalle();
        }

        function udpRegistroEnlaceUpdate() {             
            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });        
        }

        function udpListaEnlaceUpdate() {
            formatoGrillaEnlace();
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

        /* Dar formato a la grilla. */
        function formatoGrillaDetalle(){
            // Setup - add a text input to each footer cell
            $('#grwListaDetalle thead tr').clone(true).appendTo( '#grwListaDetalle thead' );
            $('#grwListaDetalle thead tr:eq(1) th').each( function (i) {
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
        
            var table = $('#grwListaDetalle').DataTable( {
                orderCellsTop: true
            } );
        }

        /* Dar formato a la grilla. */
        function formatoGrillaEnlace(){
            // Setup - add a text input to each footer cell
            $('#grwListaEnlace thead tr').clone(true).appendTo( '#grwListaEnlace thead' );
            $('#grwListaEnlace thead tr:eq(1) th').each( function (i) {
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
        
            var table = $('#grwListaEnlace').DataTable( {
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
            if (ctl.id == 'btnGuardarDetalle') {
                makeSafe();    
            }

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
                estadoTabDetalle('D');
                estadoTabEnlace('D');

            }else if (tabActivo == 'registro-tab'){
                //HABILITAR
                estadoTabRegistro('H');

                //DESHABILITAR
                estadoTabListado('D');
                estadoTabDetalle('D');
                estadoTabEnlace('D');
            }else if (tabActivo == 'detalle-tab'){
                //HABILITAR
                estadoTabDetalle('H');

                //DESHABILITAR
                estadoTabListado('D');
                estadoTabRegistro('D');                
                estadoTabEnlace('D');
            }else if (tabActivo == 'enlace-tab'){
                //HABILITAR
                estadoTabEnlace('H');

                //DESHABILITAR
                estadoTabListado('D');
                estadoTabRegistro('D'); 
                estadoTabDetalle('D');                               
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

        function estadoTabDetalle(estado) {
            if (estado == 'H') {
                $("#detalle-tab").removeClass("disabled");
                $("#detalle-tab").addClass("active");
                $("#detalle").addClass("show");
                $("#detalle").addClass("active");
            } else {
                $("#detalle-tab").removeClass("active");
                $("#detalle-tab").addClass("disabled");
                $("#detalle").removeClass("show");
                $("#detalle").removeClass("active");
            }
        }

        function estadoTabEnlace(estado) {
            if (estado == 'H') {
                $("#enlace-tab").removeClass("disabled");
                $("#enlace-tab").addClass("active");
                $("#enlace").addClass("show");
                $("#enlace").addClass("active");
            } else {
                $("#enlace-tab").removeClass("active");
                $("#enlace-tab").addClass("disabled");
                $("#enlace").removeClass("show");
                $("#enlace").removeClass("active");
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

                case 'RegistroDetalle':                      
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

        /*Permite codificar contenido html*/
        function makeSafe() {            
            //var contenido = window.escape($('#divMensaje').summernote('code'));
            var contenido = encodeURI($('#divCuerpoDetalle').summernote('code'));                           
            $("#<%=txtCuerpoDetalle.ClientID%>").val(contenido);
        };  

        function setCodigoCuerpoDetalle(contenido){
            $('#divCuerpoDetalle').summernote('code', contenido);
        }

        function divCuerpoDetalleReadonly(tipo){
            if(tipo == "S"){
                $('#divContenedorCuerpo').addClass('div-readonly');
            }else{
                $('#divContenedorCuerpo').removeClass('div-readonly');
            }            
        }
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmGestionRecursoVirtual" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">GESTIÓN DE RECURSOS VIRTUALES</div>
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
                    <a href="#detalle" id="detalle-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="detalle" aria-selected="false">Detalle</a>
                </li>
                <li class="nav-item">
                    <a href="#enlace" id="enlace-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="enlace" aria-selected="false">Enlace</a>
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
                                            <label for="cmbTipoRepositorioFiltro" class="col-sm-2 col-form-label form-control-sm">Tipo:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoRepositorioFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>
                                            </div>
                                            <label for="cmbDisciplinaFiltro" class="col-sm-1 col-form-label form-control-sm">Disciplina:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbDisciplinaFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>
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
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar un nuevo recurso virtual?', 'warning');">
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
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_rvi, nombre_rvi"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="nombre_rvi" HeaderText="RECURSO" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="nombre_tipo" HeaderText="TIPO" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="nombre_disciplina" HeaderText="DISCIPLINA" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="tienen_acceso" HeaderText="ACCESO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="contar_visita" HeaderText="VISITA" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="estado_recurso" HeaderText="ESTADO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar recurso"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el recurso seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDetalle" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Detalle" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Agregar detalle"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea agregar un detalle al recurso seleccionado?', 'warning');">
                                                    <span><i class="fas fa-list-alt"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnInactivar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Inactivar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Inactivar recurso"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea inactivar el recurso seleccionado?', 'error');">
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
                                    <div class="card-header">Datos del Recurso Virtual</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="cmbTipoRepositorio" class="col-sm-2 col-form-label form-control-sm">Tipo:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbTipoRepositorio" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>                                                
                                            </div>
                                            <label for="cmbDisciplina" class="col-sm-1 col-form-label form-control-sm">Disciplina:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbDisciplina" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>                                                
                                            </div>
                                        </div>
                                        <div class="row">                                            
                                            <label for="txtNombre" class="col-sm-2 col-form-label form-control-sm">Nombre:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtNombre" runat="server" MaxLength="250" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                            </div>
                                            <label for="cmbEstado" class="col-sm-1 col-form-label form-control-sm">Estado:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbEstado" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="A">ACTIVO</asp:ListItem>
                                                    <asp:ListItem Value="I">INACTIVO</asp:ListItem> 
                                                </asp:DropDownList>                                                
                                            </div>
                                            <label for="txtOrden" class="col-sm-1 col-form-label form-control-sm">Orden:</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtOrden" onkeypress="javascript:return soloNumeros(event)" runat="server" MaxLength="300" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="cmbContarVisita" class="col-sm-2 col-form-label form-control-sm">Contar Visita:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbContarVisita" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="S">SI</asp:ListItem>
                                                    <asp:ListItem Value="N">NO</asp:ListItem> 
                                                </asp:DropDownList>                                                
                                            </div>                                            
                                            <label for="cmbBiblioteca" class="col-sm-2 col-form-label form-control-sm">Biblioteca:</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="cmbBiblioteca" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>                                                
                                            </div>  
                                        </div>
                                        <div class="row">
                                            <label for="cmbAcceso" class="col-sm-2 col-form-label form-control-sm">Acceso:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbAcceso" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="P">PERSONAL</asp:ListItem>
                                                    <asp:ListItem Value="A">ALUMNO</asp:ListItem> 
                                                    <asp:ListItem Value="PA">PERSONAL / ALUMNO</asp:ListItem> 
                                                </asp:DropDownList>                                                
                                            </div>
                                            <label for="txtLogo" class="col-sm-1 col-form-label form-control-sm">Logo:</label>
                                            <div class="col-sm-5">
                                                <div class="row">
                                                    <div class="col-sm-9">
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
                                            <label for="txtArchivoLogo" class="col-sm-2 col-form-label form-control-sm">Cargar Logo:</label> 
                                            <div class="col-sm-7">
                                                <asp:FileUpload ID="txtArchivoLogo" runat="server" CssClass="form-control form-control-sm" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Se recomienda que la imagen tenga una resolución de 280 X 100</label>
                                        </div>
                                    </div>
                                    <div class="card-footer" style="text-align: center;">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar el recurso virtual?', 'warning');"
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
                            </ContentTemplate>

                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnGuardar" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="tab-pane" id="detalle" role="tabpanel" aria-labelledby="detalle-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpRegistroDetalle" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div id="divTituloDetalle" runat="server" class="card-header"></div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtRecursoDetalle" class="col-sm-2 col-form-label form-control-sm">Recurso:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtRecursoDetalle" runat="server" MaxLength="300" CssClass="form-control form-control-sm" AutoComplete="off" Readonly="true"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtTituloDetalle" class="col-sm-2 col-form-label form-control-sm">Título:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtTituloDetalle" runat="server" MaxLength="300" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">                                            
                                            <label for="divCuerpoDetalle" class="col-sm-2 col-form-label form-control-sm">Cuerpo:</label>
                                            <div class="col-sm-9 div-readonly" id="divContenedorCuerpo">                                                
                                                <asp:HiddenField ID="txtCuerpoDetalle" runat="server" />
                                                <div id="divCuerpoDetalle" class="summernote"></div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="cmbAccesoDetalle" class="col-sm-2 col-form-label form-control-sm">Acceso:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbAccesoDetalle" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="P">PERSONAL</asp:ListItem>
                                                    <asp:ListItem Value="A">ALUMNO</asp:ListItem> 
                                                    <asp:ListItem Value="PA">PERSONAL / ALUMNO</asp:ListItem> 
                                                </asp:DropDownList>                                                
                                            </div>
                                            <label for="txtOrdenDetalle" class="col-sm-1 col-form-label form-control-sm">Orden:</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtOrdenDetalle" runat="server" onkeypress="javascript:return soloNumeros(event)" MaxLength="300" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-11" style="text-align: center;">
                                                <asp:LinkButton ID="btnNuevoDetalle" runat="server" CssClass="btn btn-accion btn-azul"                                                    
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar un nuevo detalle?', 'warning');">
                                                    <i class="fa fa-plus-square"></i>
                                                    <span class="text">Nuevo</span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnGuardarDetalle" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar el detalle?', 'warning');"
                                                    Visible="false">
                                                    <i class="fa fa-save"></i>
                                                    <span class="text">Guardar</span>
                                                </asp:LinkButton>    
                                                <asp:LinkButton ID="btnSalirDetalle" runat="server" CssClass="btn btn-accion btn-danger">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
                                                </asp:LinkButton> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="udpListaDetalle" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwListaDetalle" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_rvd, titulo_rvd"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="orden_rvd" HeaderText="ORDEN" ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="titulo_rvd" HeaderText="TÍTULO" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="cuerpo_rvd" HeaderText="CUERPO" ItemStyle-Width="35%" />                                        
                                        <asp:BoundField DataField="tienen_acceso" HeaderText="ACCESO" ItemStyle-Width="20%" ItemStyle-Wrap="false" />                                        
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar detalle"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el detalle seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnEnlace" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Enlace" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Agregar enlace"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea agregar un enlace al detalle seleccionado?', 'warning');">
                                                    <span><i class="fas fa-link"></i></span>
                                                </asp:LinkButton>                                                
                                                <asp:LinkButton ID="btnEliminar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Eliminar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Eliminar detalle"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el detalle seleccionado?', 'error');">
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
                <div class="tab-pane" id="enlace" role="tabpanel" aria-labelledby="enlace-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpRegistroEnlace" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div id="divTituloEnlace"  runat="server" class="card-header"></div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtRecursoEnlace" class="col-sm-2 col-form-label form-control-sm">Recurso:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtRecursoEnlace" runat="server" MaxLength="300" CssClass="form-control form-control-sm" AutoComplete="off" Readonly="true"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtDetalleEnlace" class="col-sm-2 col-form-label form-control-sm">Detalle:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtDetalleEnlace" runat="server" MaxLength="300" CssClass="form-control form-control-sm" AutoComplete="off" Readonly="true"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtDescripcionEnlace" class="col-sm-2 col-form-label form-control-sm">Descripción:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtDescripcionEnlace" runat="server" MaxLength="200" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="cmbAccesoEnlace" class="col-sm-2 col-form-label form-control-sm">Acceso:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbAccesoEnlace" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="P">PERSONAL</asp:ListItem>
                                                    <asp:ListItem Value="A">ALUMNO</asp:ListItem> 
                                                    <asp:ListItem Value="PA">PERSONAL / ALUMNO</asp:ListItem> 
                                                </asp:DropDownList>                                                
                                            </div>
                                            <label for="txtOrdenEnlace" class="col-sm-2 col-form-label form-control-sm">Orden:</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtOrdenEnlace" runat="server" onkeypress="javascript:return soloNumeros(event)" MaxLength="300" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="cmbContarVisitaEnlace" class="col-sm-2 col-form-label form-control-sm">Contar Visita:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbContarVisitaEnlace" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="S">SI</asp:ListItem>
                                                    <asp:ListItem Value="N">NO</asp:ListItem> 
                                                </asp:DropDownList>                                                
                                            </div>                                            
                                            <label for="cmbBibliotecaEnlace" class="col-sm-1 col-form-label form-control-sm">Biblioteca:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbBibliotecaEnlace" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>                                                
                                            </div>  
                                        </div>
                                        <div class="row">
                                            <label for="cmbTipoEnlace" class="col-sm-2 col-form-label form-control-sm">Tipo:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoEnlace" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="L">LINK</asp:ListItem>
                                                    <asp:ListItem Value="D">DOCUMENTO</asp:ListItem> 
                                                </asp:DropDownList>                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtLinkEnlace" class="col-sm-2 col-form-label form-control-sm">Link:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtLinkEnlace" runat="server" MaxLength="500" CssClass="form-control form-control-sm" AutoComplete="off" Placeholder="http://mienlace.com"/>
                                            </div>
                                        </div>
                                        <div class="row"> 
                                            <label for="txtArchivoEnlace" class="col-sm-2 col-form-label form-control-sm">Cargar Archivo:</label> 
                                            <div class="col-sm-7">
                                                <asp:FileUpload ID="txtArchivoEnlace" runat="server" CssClass="form-control form-control-sm" />
                                            </div>
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-9" style="text-align: center;">
                                                <asp:LinkButton ID="btnNuevoEnlace" runat="server" CssClass="btn btn-accion btn-azul"                                                    
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar un nuevo enlace?', 'warning');">
                                                    <i class="fa fa-plus-square"></i>
                                                    <span class="text">Nuevo</span>
                                                </asp:LinkButton>                                                
                                                <asp:LinkButton ID="btnGuardarEnlace" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar el enlace?', 'warning');"
                                                    OnClick="btnGuardarEnlace_Click"
                                                    Visible="false">
                                                    <i class="fa fa-save"></i>
                                                    <span class="text">Guardar</span>
                                                </asp:LinkButton>    
                                                <asp:LinkButton ID="btnSalirEnlace" runat="server" CssClass="btn btn-accion btn-danger">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>

                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnGuardarEnlace" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="udpListaEnlace" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwListaEnlace" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_rve"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="orden_rve" HeaderText="ORDEN" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="descripcion_rve" HeaderText="DESCRIPCIÓN" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="enlace" HeaderText="ENLACE" ItemStyle-Width="20%"/>                                        
                                        <asp:BoundField DataField="tienen_acceso" HeaderText="ACCESO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="contar_visita" HeaderText="VISITA" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="IdArchivosCompartidos" HeaderText="ARCHIVO" ItemStyle-Width="10%" ItemStyle-Wrap="false" Visible="false" />                                        
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar enlace"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el enlace?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnLink" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Link" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Abrir enlace"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea abrir el enlace asociado?', 'warning');">
                                                    <span><i class="fas fa-external-link-alt"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnArchivo" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Archivo" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Descargar archivo"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea descargar el archivo asociado?', 'warning');">
                                                    <span><i class="fas fa-file-download"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Eliminar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Eliminar enlace"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el enlace?', 'error');">
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
                                        <asp:Image ID="imgLogo" runat="server" ImageUrl="" Width="280px" Height="95px" />
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
                    break;

                case 'btnGuardarDetalle':
                    AlternarLoading(false, 'RegistroDetalle');
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

                case 'btnGuardar':
                    AlternarLoading(true, 'Registro');
                
                case 'btnGuardarDetalle':
                    AlternarLoading(true, 'RegistroDetalle');
                    break;
            }
        }); 
    </script>
</body>
</html>
