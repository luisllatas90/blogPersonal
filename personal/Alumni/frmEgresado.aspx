<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEgresado.aspx.vb" Inherits="Alumni_frmEgresado" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Egresados</title>
    <!-- Estilos externos -->         
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="css/datatables/jquery.dataTables.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote-bs4.css">
    <link rel="stylesheet" href="css/sweetalert/sweetalert2.min.css">

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="css/estilos.css?1">

    <!-- Scripts externos -->    
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>    
    <script src="js/popper.js"></script>  
    <script src="js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="js/sweetalert/sweetalert2.js"></script>
    <script src="js/datatables/jquery.dataTables.min.js?1"></script>    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote-bs4.js"></script>

    <!-- Scripts propios -->    
    <script src="js/funciones.js?1"></script>  

    <script type="text/javascript">
        var selTodos = true;        

        /* Scripts a ejecutar al actualizar los paneles.*/
        function udpFiltrosUpdate() {
            $('#cmbNivelFiltro').selectpicker({
                size: 6,
            });

            $('#cmbModalidadFiltro').selectpicker({
                size: 6,
            });

            $('#cmbFacultadFiltro').selectpicker({
                size: 6,
            });

            $('#cmbCarreraFiltro').selectpicker({
                size: 6,
            });
            $('#cmbSexoFiltro').selectpicker({
                size: 6,
            });

            $('#cmbAnioEgresoFiltro').selectpicker({
                size: 6,
            });

            $('#cmbAnioBachillerFiltro').selectpicker({
                size: 6,
            });

            $('#cmbAnioTituloFiltro').selectpicker({
                size: 6,
            });
        }

        function udpListaUpdate() {
            formatoGrilla();
        }

        function udpRegistroUpdate() {
            $('#cmbTelefonoEgresado').selectpicker({
                size: 6,
            });       
            
            udpInformacionLaboralUpdate();
        }

        function udpInformacionLaboralUpdate() {
            $('#cmbTelefonoEmpresa').selectpicker({
                size: 6,
            }); 
        }

        function udpRegistroEmpresaUpdate() {
            $('#cmbTelRegEmpresa').selectpicker({
                size: 6,
            }); 
        }

        function udpEnvioUpdate() { 
            $('.summernote').summernote('destroy');            
            $('.summernote').summernote({
                placeholder: 'Redacta tu mensaje...',
                tabsize: 2,
                height: 100,
                focus: true
            });         
        }

        function udpListaEmpresaUpdate() { 
            formatoGrillaEmpresa();
        }

        /* Dar formato a la grilla. */
        function formatoGrilla(){
            // Setup - add a text input to each footer cell
            $('#grwLista thead tr').clone(true).appendTo( '#grwLista thead' );
            $('#grwLista thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( tableEgresado.column(i).search() !== this.value ) {
                        tableEgresado
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            tableEgresado = $('#grwLista').DataTable( {
                orderCellsTop: true                 
                //fixedHeader: true
            } );
        }

        function formatoGrillaEmpresa(){
            // Setup - add a text input to each footer cell
            $('#grwListaEmpresa thead tr').clone(true).appendTo( '#grwListaEmpresa thead' );
            $('#grwListaEmpresa thead tr:eq(1) th').each( function (i) {
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
        
            var table = $('#grwListaEmpresa').DataTable( {
                orderCellsTop: true,                
                fixedHeader: true
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
            if (ctl.id == 'btnEnviarMensaje') {
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
                    if (ctl.id == "btnEnviarMensaje"){
                        AlternarLoading(false, 'EnviarMensaje');                                                                        
                    } else if (ctl.id == "btnEnviar"){
                        tableEgresado.page.len(-1).draw();
                    }

                    window.location.href = defaultAction;
                    return true;
                } else {
                    return false;
                }
            }).catch(swal.noop);
        }        

        /* Abrir y cerrar el modal. */        
        function openModal() {
            $('#mdlBuscarEmpresa').modal('show');
        }        

        function closeModal() {
            $('#mdlBuscarEmpresa').modal('hide');
        }

        /* Flujo de tabs de la página principal. */
        function flujoTabs(tabActivo) {    
            if (tabActivo == 'listado-tab') {     
                //HABILITAR  
                estadoTabListado('H');            

                //DESHABILITAR 
                estadoTabRegistro('D');
                estadoTabEnvio('D');                

            }else if (tabActivo == 'registro-tab'){
                //HABILITAR             
                estadoTabRegistro('H');

                //DESHABILITAR 
                estadoTabListado('D');
                estadoTabEnvio('D'); 

            }else if (tabActivo == 'envio-tab'){
                //HABILITAR             
                estadoTabEnvio('H');

                //DESHABILITAR 
                estadoTabListado('D');
                estadoTabRegistro('D');                
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

        function estadoTabEnvio(estado) {
            if (estado == 'H') {
                $("#envio-tab").removeClass("disabled");
                $("#envio-tab").addClass("active");
                $("#envio").addClass("show");
                $("#envio").addClass("active");
            } else {
                $("#envio-tab").removeClass("active");
                $("#envio-tab").addClass("disabled");
                $("#envio").removeClass("show");
                $("#envio").removeClass("active");
            }
        }

        /* Flujo de tabs del modal. */
        function flujoTabsModal(tabActivo) {
            if (tabActivo == 'listaEmpresa-tab') {     
                //HABILITAR  
                estadoTabListaEmpresa('H');            

                //DESHABILITAR 
                estadoTabRegistroEmpresa('D');                

            }else if (tabActivo == 'registroEmpresa-tab'){
                //HABILITAR             
                estadoTabRegistroEmpresa('H');

                //DESHABILITAR 
                estadoTabListaEmpresa('D');                

            }
        }

        function estadoTabListaEmpresa(estado) {
            if (estado == 'H') {
                $("#listaEmpresa-tab").removeClass("disabled");
                $("#listaEmpresa-tab").addClass("active");
                $("#listaEmpresa").addClass("show");
                $("#listaEmpresa").addClass("active");
            } else {
                $("#listaEmpresa-tab").removeClass("active");
                $("#listaEmpresa-tab").addClass("disabled");
                $("#listaEmpresa").removeClass("show");
                $("#listaEmpresa").removeClass("active");
            }
        }

        function estadoTabRegistroEmpresa(estado) {
            if (estado == 'H') {
                $("#registroEmpresa-tab").removeClass("disabled");
                $("#registroEmpresa-tab").addClass("active");
                $("#registroEmpresa").addClass("show");
                $("#registroEmpresa").addClass("active");
            } else {
                $("#registroEmpresa-tab").removeClass("active");
                $("#registroEmpresa-tab").addClass("disabled");
                $("#registroEmpresa").removeClass("show");
                $("#registroEmpresa").removeClass("active");
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
                case 'ListaEmpresa':
                    $loadingGif = $('#loading-listaEmpresa');
                    $elemento = $('#udpListaEmpresa');
                    break;
                case 'RegistroEmpresa':                                        
                    $loadingGif = $('.loader');
                    break;           
                case 'EnviarMensaje':
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

        /*Seleccionar Egresados para Enviar Mensaje*/
        function marcarTodosEgresados() {             
            var rows = tableEgresado.rows({ 'search': 'applied' }).nodes();            
            $('input[type="checkbox"]', rows).prop('checked', selTodos);   

            if (selTodos) {
                selTodos = false;
            } else {
                selTodos = true;
            }         
        }

        /*Permite codificar contenido html*/
        function makeSafe() {            
            //var contenido = window.escape($('#divMensaje').summernote('code'));
            var contenido = encodeURI($('#divMensaje').summernote('code'));                           
            $("#<%=txtMensaje.ClientID%>").val(contenido);
        };       

        /*Mostrar el boton Seleccionar Todos*/ 
        function mostrarBotonTodos() {
            $('#sel-todos').removeClass("oculto");
        }
    </script>
</head>
<body>    
    <div class="loader"></div>

    <form id="frmEgresado" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>   
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>
        
        <div class="container-fluid">                            
            <div class="card div-title">                        
                <div class="row title">GESTIÓN DE EGRESADOS</div>
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
                    <a href="#envio" id="envio-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="envio" aria-selected="false">Enviar Mensaje</a>
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
                                            <label for="cmbNivelFiltro" class="col-sm-2 col-form-label form-control-sm">Nivel:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbNivelFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbModalidadFiltro" class="col-sm-1 col-form-label form-control-sm">Modalidad:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbModalidadFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbFacultadFiltro" class="col-sm-1 col-form-label form-control-sm">Facultad:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbFacultadFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>                                        
                                        </div>                                    
                                        <div class="row">
                                            <label for="cmbCarreraFiltro" class="col-sm-2 col-form-label form-control-sm">Carrera:</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="cmbCarreraFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>   
                                            <label for="cmbSexoFiltro" class="col-sm-1 col-form-label form-control-sm">Sexo:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbSexoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>                                                                               
                                        </div>
                                        <div class="row">
                                            <label for="cmbAnioEgresoFiltro" class="col-sm-2 col-form-label form-control-sm">Egreso:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbAnioEgresoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div> 
                                            <label for="cmbAnioBachillerFiltro" class="col-sm-1 col-form-label form-control-sm">Bachiller:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbAnioBachillerFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbAnioTituloFiltro" class="col-sm-1 col-form-label form-control-sm">Título:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbAnioTituloFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>                                          
                                        </div>
                                        <div class="row">
                                            <label for="txtNombreFiltro" class="col-sm-2 form-control-sm">Nombre / DNI:</label>
                                            <div class="col-sm-5">                                                                                
                                                <asp:TextBox ID="txtNombreFiltro" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                           
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-accion btn-celeste">
                                                    <i class="fa fa-sync-alt"></i>
                                                    <span class="text">Listar</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnEnviar" runat="server" CssClass="btn btn-accion btn-azul"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea enviar un mensaje a los egresados seleccionados?', 'warning');">
                                                    <i class="fa fa-envelope-square"></i>
                                                    <span class="text">Enviar Mensaje</span>
                                                </asp:LinkButton>   
                                                <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea exportar la lista de egresados?', 'warning');">
                                                    <i class="fa fa-envelope-square"></i>
                                                    <span class="text">Exportar</span>
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
                                <div id="sel-todos" class="oculto">
                                    <asp:LinkButton ID="btnElegir" runat="server" CssClass="btn btn-accion btn-naranja"
                                        OnClientClick="marcarTodosEgresados();">
                                        <i class="fa fa-check-square"></i>
                                        <span class="text">Todos</span>
                                    </asp:LinkButton>
                                </div>
                                <br/>
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="true"
                                    CssClass="display table table-sm" DataKeyNames="codigo_ega, codigo_Pso, correo_personal, correo_profesional">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SEL.">
                                            <ItemTemplate>                
                                                <asp:CheckBox ID="chkElegir" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nivel" HeaderText="NIVEL" />
                                        <asp:BoundField DataField="modalidad" HeaderText="MODALIDAD" />
                                        <asp:BoundField DataField="apellidos" HeaderText="APELLIDOS" />
                                        <asp:BoundField DataField="nombres" HeaderText="NOMBRES" />
                                        <asp:BoundField DataField="facultad" HeaderText="FACULTAD" />
                                        <asp:BoundField DataField="escuela_profesional" HeaderText="CARRERA" />
                                        <asp:BoundField DataField="sexo" HeaderText="SEXO" />
                                        <asp:BoundField DataField="anio_egreso" HeaderText="AÑO EGR." />
                                        <asp:BoundField DataField="anio_bachiller" HeaderText="AÑO BACH." />
                                        <asp:BoundField DataField="anio_titulo" HeaderText="AÑO TIT." />
                                        <asp:BoundField DataField="correo" HeaderText="CORREO PER." />
                                        <asp:TemplateField HeaderText="OPE.">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar egresado"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
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
                                    <div class="card-header">Información del Egresado</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtNombre" class="col-sm-1 offset-sm-1 col-form-label form-control-sm">Nombre:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtNivel" class="col-sm-1 col-form-label form-control-sm">Nivel:</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtNivel" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtModalidad" class="col-sm-1 col-form-label form-control-sm">Modalidad:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtModalidad" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtFacultad" class="col-sm-1 offset-sm-1 col-form-label form-control-sm">Facultad:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtFacultad" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtCarrera" class="col-sm-1 col-form-label form-control-sm">Carrera:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtCarrera" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">    
                                            <label for="txtAnioEgreso" class="col-sm-1 offset-sm-1 col-form-label form-control-sm">Año Egreso:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtAnioEgreso" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                        
                                            <label for="txtAnioBachiller" class="col-sm-2 col-form-label form-control-sm">Año Bachiller:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtAnioBachiller" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtAnioTitulo" class="col-sm-1 col-form-label form-control-sm">Año Título:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtAnioTitulo" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br/>
                                <div class="card">
                                    <div class="card-header">Información de Contacto del Egresado</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtCorreoPersonal" class="col-sm-1 offset-sm-1 col-form-label form-control-sm">Correo Personal<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtCorreoPersonal" runat="server" MaxLength="100" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                            </div>                                            
                                            <label for="txtCorreoProfesional" class="col-sm-1 col-form-label form-control-sm">Correo Profesional:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtCorreoProfesional" runat="server" MaxLength="100" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                            </div>                                            
                                        </div>
                                        <div class="row">
                                            <label for="cmbTelefonoEgresado" class="col-sm-1 offset-sm-1 col-form-label form-control-sm">Teléfono:</label>
                                            <div class="col-sm-3">
                                                <div class="row">                                                    
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="cmbTelefonoEgresado" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtTelefonoEgresado" runat="server" MaxLength="7" CssClass="form-control form-control-sm" 
                                                            onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <label for="txtCelular01Egresado" class="col-sm-1 col-form-label form-control-sm">Celular 01:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtCelular01Egresado" runat="server" MaxLength="9" CssClass="form-control form-control-sm"
                                                    onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                            </div>
                                            <label for="txtCelular02Egresado" class="col-sm-1 col-form-label form-control-sm">Celular 02:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtCelular02Egresado" runat="server" MaxLength="9" CssClass="form-control form-control-sm"
                                                    onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br/>
                                <asp:UpdatePanel ID="udpInformacionLaboral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="card">
                                            <div class="card-header">Información Laboral</div>
                                            <div class="card-body">
                                                <div class="row">         
                                                    <label for="chkLaboraActual" class="col-sm-2 col-form-label form-control-sm">Labora Actualmente:</label>                                   
                                                    <div class="col-sm-1">                                                
                                                        <asp:CheckBox ID="chkLaboraActual" AutoPostBack="True" OnCheckedChanged="chkLaboraActual_CheckedChanged" runat="server"/>
                                                    </div> 
                                                </div>
                                                <div class="row">
                                                    <asp:HiddenField ID="txtCodigoEmp" runat="server" />
                                                    <label for="txtCentroLaboral" class="col-sm-2 col-form-label form-control-sm">Centro Laboral<span class="requerido">(**)</span>:</label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtCentroLaboral" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                                    </div>  
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-accion btn-celeste">
                                                            <i class="fa fa-search-plus"></i>
                                                            <span class="text">Buscar</span>
                                                        </asp:LinkButton> 
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label for="txtCargo" class="col-sm-1 offset-sm-1 col-form-label form-control-sm">Cargo<span class="requerido">(**)</span>:</label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtCargo" runat="server" MaxLength="100" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                                    </div>      
                                                    <label for="cmbTelefonoEmpresa" class="col-sm-1 col-form-label form-control-sm">Teléfono:</label>
                                                    <div class="col-sm-3">
                                                        <div class="row">                                                    
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="cmbTelefonoEmpresa" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtTelefonoEmpresa" runat="server" MaxLength="7" CssClass="form-control form-control-sm"
                                                                    onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                            </div>
                                                        </div>
                                                    </div>                                                                                                                                         
                                                </div>
                                                <div class="row">
                                                    <label for="txtCorreoEmpresa" class="col-sm-1 offset-sm-1 col-form-label form-control-sm">Correo:</label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtCorreoEmpresa" runat="server" MaxLength="100" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                                    </div>                                                        
                                                    <label for="txtCelularEmpresa" class="col-sm-1 col-form-label form-control-sm">Celular:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtCelularEmpresa" runat="server" MaxLength="9" CssClass="form-control form-control-sm"
                                                            onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                    </div>                                                     
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <br/>
                                <div class="row">
                                    <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                    <label class="col-sm-12 label-requerido"><span class="requerido">(**)</span> Campos obligatorios en caso labore actualmente</label>
                                </div>  
                                <hr/>                                 
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center;">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar la información del egresado?', 'warning');">
                                            <i class="fa fa-save"></i>
                                            <span class="text">Guardar</span>
                                        </asp:LinkButton>                                                                     
                                        <asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-accion btn-rojo">
                                            <i class="fa fa-sign-out-alt"></i>
                                            <span class="text">Salir</span>
                                        </asp:LinkButton>                                                                                              
                                    </div>                                 
                                </div>                               
                                <br/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>                     
                </div>
                <div class="tab-pane" id="envio" role="tabpanel" aria-labelledby="envio-tab">
                    <div class="panel-cabecera">
                        <div class="card">
                            <div class="card-header">Envío de Mensajes Múltiples</div>
                            <div class="card-body">                                                              

                                <asp:UpdatePanel ID="udpEnvio" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="row">
                                            <label class="col-sm-1 col-form-label form-control-sm">Enviar:</label>
                                            <asp:Label ID="lblDestinatarios" Text="Número de Destinatarios" CssClass="col-sm-6 col-form-label form-control-sm" runat="server"/>
                                        </div>
                                        <div class="row">
                                            <label for="txtAsunto" class="col-sm-1 col-form-label form-control-sm">Asunto<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtAsunto" runat="server" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="divMensaje" class="col-sm-1 col-form-label form-control-sm">Mensaje<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-10">
                                                <asp:HiddenField ID="txtMensaje" runat="server" />
                                                <div id="divMensaje" class="summernote"></div>
                                            </div>                                            
                                        </div>       
                                        <div class="row">
                                            <label for="txtArchivo" class="col-sm-1 col-form-label form-control-sm">Adjunto:</label>
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="txtArchivo" runat="server" CssClass="form-control form-control-sm" />
                                            </div>
                                        </div> 
                                        <div class="row">
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                        </div>                                         
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: center;">
                                                <asp:LinkButton ID="btnEnviarMensaje" runat="server" CssClass="btn btn-accion btn-verde" 
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea enviar el mensaje a los egresados?', 'warning');"
                                                    OnClick="btnEnviarMensaje_Click">
                                                    <i class="fa fa-paper-plane"></i>
                                                    <span class="text">Enviar</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnSalirMensaje" runat="server" CssClass="btn btn-accion btn-rojo">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                </asp:LinkButton>                                                                                              
                                            </div>                                 
                                        </div>                                          
                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnEnviarMensaje" />
                                    </Triggers>                                    
                                </asp:UpdatePanel>  
                            </div>
                        </div>                        
                    </div>
                </div>                
            </div>
        </div>
        
        <!-- Modal Busqueda de Empresas -->
        <div id="mdlBuscarEmpresa" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">EMPRESAS</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div>   
                    <div class="modal-body">
                        <div class="container-fluid">                            
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a href="#listaEmpresa" id="listaEmpresa-tab" class="nav-link active" data-toggle="tab" role="tab"
                                        aria-controls="listaEmpresa" aria-selected="true">Listado</a>
                                </li>
                                <li class="nav-item">
                                    <a href="#registroEmpresa" id="registroEmpresa-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                                        aria-controls="registroEmpresa" aria-selected="false">Registro</a>
                                </li>
                            </ul>
                            <div class="tab-content" id="contentTabsEmpresa">
                                <div class="tab-pane show active" id="listaEmpresa" role="tabpanel" aria-labelledby="listaEmpresa-tab">                                    
                                    <div class="panel-cabecera">
                                        <asp:UpdatePanel ID="udpFiltrosEmpresa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="card">
                                                    <div class="card-header">Filtros de Búsqueda</div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <label for="txtNombreComercialFiltro" class="col-sm-2 form-control-sm">Nombre / RUC:</label>
                                                            <div class="col-sm-5">                                                                                
                                                                <asp:TextBox ID="txtNombreComercialFiltro" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                                            </div>  
                                                        </div>
                                                        <hr/>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <asp:LinkButton ID="btnListarEmpresa" runat="server" CssClass="btn btn-accion btn-celeste">
                                                                    <i class="fa fa-sync-alt"></i>
                                                                    <span class="text">Listar</span>
                                                                </asp:LinkButton> 
                                                                <asp:LinkButton ID="btnNuevaEmpresa" runat="server" CssClass="btn btn-accion btn-verde">
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
                                        <div id="loading-listaEmpresa" class="loading oculto">
                                            <img src="img/loading.gif">
                                        </div>                 
                                        <asp:UpdatePanel ID="udpListaEmpresa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:GridView ID="grwListaEmpresa" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_emp, nombreComercial_emp"
                                                    CssClass="display table table-sm" GridLines="None">
                                                    <Columns>                                    
                                                        <asp:BoundField DataField="nombreComercial_emp" HeaderText="EMPRESA" ItemStyle-Width="80%" ItemStyle-Wrap="false"/>
                                                        <asp:TemplateField HeaderText="SEL." ItemStyle-Width="20%" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnSeleccionar" runat="server" 
                                                                    CommandName="Seleccionar" 
                                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                                    CssClass="btn btn-primary btn-sm" 
                                                                    ToolTip="Seleccionar empresa"
                                                                    OnClientClick="return alertConfirm(this, event, '¿Desea enviar la empresa seleccionada?', 'warning');">
                                                                    <span><i class="fa fa-check-circle"></i></span>
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
                                <div class="tab-pane" id="registroEmpresa" role="tabpanel" aria-labelledby="registroEmpresa-tab">
                                    <div class="panel-cabecera">                                         
                                        <asp:UpdatePanel ID="udpRegistroEmpresa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="card">
                                                    <div class="card-header">Registro Rápido de Empresa</div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <label for="txtNombreComercial" class="col-sm-2 col-form-label form-control-sm">Nombre<span class="requerido">(*)</span>:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtNombreComercial" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label for="txtCorreoRegEmpresa" class="col-sm-2 col-form-label form-control-sm">Correo:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtCorreoRegEmpresa" runat="server" MaxLength="100" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label for="cmbTelRegEmpresa" class="col-sm-2 col-form-label form-control-sm">Teléfono:</label>
                                                            <div class="col-sm-6">
                                                                <div class="row">                                                    
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="cmbTelRegEmpresa" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtTelRegEmpresa" runat="server" MaxLength="6" CssClass="form-control form-control-sm"
                                                                            onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                                    </div>
                                                                </div>
                                                            </div> 
                                                        </div>                                                        
                                                        <div class="row">
                                                            <label for="txtCelRegEmpresa" class="col-sm-2 col-form-label form-control-sm">Celular:</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtCelRegEmpresa" runat="server" MaxLength="9" CssClass="form-control form-control-sm"
                                                                    onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                            </div>  
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                                        </div>                                                         
                                                    </div>
                                                    <div class="modal-footer">                                
                                                        <asp:LinkButton ID="btnGuardarEmpresa" runat="server" CssClass="btn btn-accion btn-verde" 
                                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar la empresa?', 'warning');">
                                                            <i class="fa fa-save"></i>
                                                            <span class="text">Guardar</span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btnSalirEmpresa" runat="server" CssClass="btn btn-accion btn-rojo">
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
                    </div>                 
                </div>                
            </div>
        </div>               
    </form>

    <script type="text/javascript">
        var controlId = '';
        var tableEgresado;

        /* Ejecutar funciones una vez cargada en su totalidad la página web. */        
        $(document).ready(function() {                                        
            udpFiltrosUpdate();
            udpRegistroUpdate();
            
            /*Ocultar cargando*/
            $(".loader").fadeOut("slow");                              
        });

        /* Mostrar y ocultar gif al realizar un procesamiento. */        
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            switch (controlId) {                
                case 'btnListar':
                    AlternarLoading(false, 'Lista');  
                    $('#sel-todos').addClass("oculto");
                    break;
                case 'btnGuardar':
                    AlternarLoading(false, 'Registro');                    
                    break;
                case 'btnBuscar':
                    $('#udpListaEmpresa').addClass("oculto");
                    break;
                case 'btnListarEmpresa':
                    AlternarLoading(false, 'ListaEmpresa');                    
                    break;      
                case 'btnGuardarEmpresa':
                    AlternarLoading(false, 'RegistroEmpresa');                    
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
                    $('#sel-todos').removeClass("oculto");              
                    break;  
                case 'btnGuardar':
                    AlternarLoading(true, 'Registro');    
                    $('#sel-todos').removeClass("oculto");                
                    break;
                case 'btnListarEmpresa':
                    AlternarLoading(true, 'ListaEmpresa');                    
                    break;   
                case 'btnGuardarEmpresa':
                    AlternarLoading(true, 'RegistroEmpresa');                    
                    break;                            
            }
        });
    </script>     
</body>
</html>
