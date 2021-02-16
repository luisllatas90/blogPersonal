<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConformidadRecursos.aspx.vb" Inherits="DisenioPuestos_frmConformidadRecursos" %>

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

        function udpTareaUpdate() {
            var table = $('#grwTarea').DataTable( {
                orderCellsTop: true
            } );
        }

        function udpT1Update() {    
            var table = $('#grwT1').DataTable( {
                orderCellsTop: true
            } );
        }

        function udpT2Update() {
            var table = $('#grwT2').DataTable( {
                orderCellsTop: true
            } );
        }
        function udpT3Update() {
            var table = $('#grwT3').DataTable( {
                orderCellsTop: true
            } );
        }
        function udpT4Update() {
            var table = $('#grwT4').DataTable( {
                orderCellsTop: true
            } );
        }

        function udpCompetenciaUpdate() {
            var table = $('#grwCompetencia').DataTable( {
                orderCellsTop: true,
                order: [[ 0, "desc" ]]
            } );
        }

        function udpRecomendacionUpdate() {
            var table = $('#grwRecomendacion').DataTable( {
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
                estadoTabFicha('D'); 
            }else if (tabActivo == 'registro-tab'){
                //HABILITAR
                estadoTabRegistro('H');

                //DESHABILITAR
                estadoTabListado('D');
                estadoTabFicha('D'); 
            }else if (tabActivo == 'ficha-tab'){
                //HABILITAR
                estadoTabFicha('H');

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

        function estadoTabFicha(estado) {
            if (estado == 'H') {
                $("#ficha-tab").removeClass("disabled");
                $("#ficha-tab").addClass("active");
                $("#ficha").addClass("show");
                $("#ficha").addClass("active");
            } else {
                $("#ficha-tab").removeClass("active");
                $("#ficha-tab").addClass("disabled");
                $("#ficha").removeClass("show");
                $("#ficha").removeClass("active");
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
                    
                case 'ListaFicha':
                    $loadingGif = $('#loading-ficha');                    
                    $elemento = $('#udpFicha');
                    break; 

                case 'ListaTarea':
                    $loadingGif = $('#loading-tarea');                    
                    $elemento = $('#udpTarea');
                    break; 

                case 'ListaCompetencia':
                    $loadingGif = $('#loading-competencia');                    
                    $elemento = $('#udpCompetencia');
                    break; 

                case 'ListaRecomendacion':
                    $loadingGif = $('#loading-recomendacion');                    
                    $elemento = $('#udpRecomendacion');
                    break;

                case 'ListaT1':                  
                    $elemento = $('#udpT1');
                    break;

                case 'ListaT2':                   
                    $elemento = $('#udpT2');
                    break;

                case 'ListaT3':                  
                    $elemento = $('#udpT3');
                    break;

                case 'ListaT4':                    
                    $elemento = $('#udpT4');
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

        /* Abrir y cerrar el modal. */
        function openModal(elemento) {            
            switch (elemento) {                 
                case 'BuscarAlumno':                    
                    $('#mdlBuscarAlumno').modal('show');                   
                    break;    
                    
                case 'Observacion':                    
                    $('#mdlObservacion').modal('show');                   
                    break;

                case 'Mensaje':                    
                    $('#mdlMensaje').modal('show');                   
                    break;
            }
        }

        function closeModal(elemento) {
            switch (elemento) { 
                case 'BuscarAlumno':
                    $('#mdlBuscarAlumno').modal('hide');                   
                    break;     
                    
                case 'Observacion':
                    $('#mdlObservacion').modal('hide');                   
                    break; 

                case 'Mensaje':
                    $('#mdlMensaje').modal('hide');                   
                    break;
            }            
        } 
        


    </script>
    <style>
        ul, #myUL {
          list-style-type: none;
        }
        
        #myUL {
          margin: 0;
          padding: 0;
        }
        
        .caret {
          cursor: pointer;
          -webkit-user-select: none; /* Safari 3.1+ */
          -moz-user-select: none; /* Firefox 2+ */
          -ms-user-select: none; /* IE 10+ */
          user-select: none;
        }
        
        .caret::before {
          content: "\25B6";
          color: black;
          display: inline-block;
          margin-right: 6px;
        }
        
        .caret-down::before {
          -ms-transform: rotate(90deg); /* IE 9 */
          -webkit-transform: rotate(90deg); /* Safari */
          transform: rotate(90deg);  
        }
        
        .nested {
          display: none;
        }
        
        .active {
          display: block;
        }
        </style>
</head>
<body>
    <div class="loader"></div>

    <form id="frmConformidadRecursos" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">CONFIRMACIÓN A RECURSOS REQUERIDOS</div>
            </div>
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="listado" aria-selected="true">Listado</a>
                </li>                
                <li class="nav-item">
                    <a href="#ficha" id="ficha-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="ficha" aria-selected="false">Análisis de puesto de trabajo</a>
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
                                            <label for="cmbTipoFiltro" class="col-sm-2 col-form-label form-control-sm">Tipo De Trabajador:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="ADMINISTRADOR">ADMINISTRADOR</asp:ListItem>
                                                    <asp:ListItem Value="DOCENTE">DOCENTE</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                            </div>
                                            <label for="cmbPuesto" class="col-sm-2 col-form-label form-control-sm">Puesto De Trabajo:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbPuesto" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="D">DECANO</asp:ListItem>
                                                    <asp:ListItem Value="A">ANALISTA PROGRAMADOR</asp:ListItem>                                                   
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
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_pue"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="puesto" HeaderText="PUESTO DE TRABAJO" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="OPCIONES" ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnGestionarFicha" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="GestionarFicha" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Ver Ficha">
                                                    <span><i class="fa fa-user-friends"></i></span>                                                
                                                </asp:LinkButton>                                                
                                                <!--  
                                                <asp:LinkButton ID="btnVer" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Ver" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Ver notificación">
                                                    <span>Ver</span>
                                                </asp:LinkButton>
                                                -->
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Editar notificación"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea aprobar el registro seleccionado?', 'warning');">
                                                    <span><i class="fas fa-check-square"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Eliminar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Eliminar notificación"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea desaprobar el registro seleccionado?', 'error');">
                                                    <span><i class="fas fa-times-circle"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="OSERVACIÓN" ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnObservar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Observar" 
                                                    CssClass="btn btn-warning btn-sm" 
                                                    ToolTip="Mensaje de la observación">
                                                    <span><i class="fas fa-arrow-circle-right"></i></span>
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
                <div class="tab-pane" id="ficha" role="tabpanel" aria-labelledby="ficha-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpFiltrosFicha" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">I. PUESTO IDENTIFICADO</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtDenominacion" class="col-sm-2 col-form-label form-control-sm">Denominación del puesto:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtDenominacion" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                         
                                        </div> 
                                        <div class="row">
                                            <label for="txtCP" class="col-sm-2 col-form-label form-control-sm">Codificación del puesto:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtCP" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtVacante" class="col-sm-1.5 col-form-label form-control-sm">N° Vacantes:</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtVacante" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtTipo" class="col-sm-2 col-form-label form-control-sm">Tipo de trabajador:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtTipo" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                         
                                        </div> 
                                        <div class="row">
                                            <label for="txtTS" class="col-sm-2 col-form-label form-control-sm">Tipo de servicio:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtTS" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                         
                                        </div> 
                                    </div>
                                    <div class="card-header">II. ORGANIZACIÓN RELACIONADA AL PUESTO IDENTIFICADO</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtReporta" class="col-sm-2 col-form-label form-control-sm">Reporta a:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtReporta" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                         
                                        </div>
                                        <div class="row">
                                            <label for="txtSupervisa" class="col-sm-2 col-form-label form-control-sm">Supervisa a:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtSupervisa" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                         
                                        </div>
                                    </div>
                                    <div class="card-header">III. RELACIONES DEL PUESTO IDENTIFICADO</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txt1" class="col-sm-2 col-form-label form-control-sm">Cliente(s) del puesto:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txt1" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>
                                        <div class="row">
                                            <label for="txt2" class="col-sm-2 col-form-label form-control-sm">Trabajo con otros(as) Departamentos/Áreas:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txt2" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>
                                        <div class="row">
                                            <label for="txt3" class="col-sm-2 col-form-label form-control-sm">Trabajo con contactos externos:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txt3" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>
                                    </div>
                                    <div class="card-header">IV. DESCRIPCIÓN DEL PUESTO IDENTIFICADO</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtT" class="col-sm-2 col-form-label form-control-sm">Total:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtT1" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off">6 </asp:TextBox>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtT2" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off">100%</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <div id="loading-tarea" class="loading oculto">
                                                <img src="img/loading.gif">
                                            </div> 
                                            <asp:UpdatePanel ID="udpTarea" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grwTarea" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_tar"
                                                        CssClass="display table table-sm" GridLines="None">
                                                        <Columns>
                                                            <asp:BoundField DataField="que" HeaderText="¿QUÉ HACE?" />
                                                            <asp:BoundField DataField="como" HeaderText="¿CÓMO LO HACE?" />
                                                            <asp:BoundField DataField="min" HeaderText="MINUTOS / SEMANA" />
                                                            <asp:BoundField DataField="tiempo" HeaderText="%TIEMPO" />   
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br/>
                                        </div>                                        
                                        <div class="row">
                                            <label for="txtProposito" class="col-sm-2 col-form-label form-control-sm">Propósito:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtProposito" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>
                                        <br/>
                                        <div class="row">
                                            <div class="col-sm-6">                                                
                                                <div class="table-responsive">
                                                    <asp:UpdatePanel ID="udpT1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="grwT1" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_t1"
                                                                CssClass="display table table-sm" GridLines="None">
                                                                <Columns>
                                                                    <asp:BoundField DataField="t1" HeaderText="FUNCIONES PERMANENTES" />  
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <br/>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="table-responsive">
                                                    <asp:UpdatePanel ID="udpT2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="grwT2" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_t2"
                                                                CssClass="display table table-sm" GridLines="None">
                                                                <Columns>
                                                                    <asp:BoundField DataField="t2" HeaderText="FUNCIONES ESPORÁDICAS" />  
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <br/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="table-responsive">
                                                    <asp:UpdatePanel ID="udpT3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="grwT3" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_t3"
                                                                CssClass="display table table-sm" GridLines="None">
                                                                <Columns>
                                                                    <asp:BoundField DataField="t3" HeaderText="CARGOS ADMINISTRATIVOS" />  
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <br/>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="table-responsive">
                                                    <asp:UpdatePanel ID="udpT4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="grwT4" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_t4"
                                                                CssClass="display table table-sm" GridLines="None">
                                                                <Columns>
                                                                    <asp:BoundField DataField="t4" HeaderText="FUNCIONES" />  
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <br/>
                                                </div>
                                            </div>
                                        </div>  
                                    </div>
                                    <div class="card-header">V. PERFIL REQUERIDO PARA EL PUESTO GENERAL</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtFormacion" class="col-sm-2 col-form-label form-control-sm">Formación:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtFormacion" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtEn1" class="col-sm-1 col-form-label form-control-sm">En:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtEn1" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtExperiencia" class="col-sm-2 col-form-label form-control-sm">Experiencia específica:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtExperiencia" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtEn2" class="col-sm-1 col-form-label form-control-sm">En:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtEn2" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>                                        
                                        <div class="row">
                                            <label for="txtFTC" class="col-sm-2 col-form-label form-control-sm">Formación Técnica Complementaria:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtFTC" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>
                                        <div class="row">
                                            <label for="txtIdioma" class="col-sm-2 col-form-label form-control-sm">Idioma:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtIdioma" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtEn3" class="col-sm-1 col-form-label form-control-sm">En:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtEn3" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>                                        
                                        <div class="row">
                                            <label for="txtMP" class="col-sm-2 col-form-label form-control-sm">Manejo de programas:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtMP" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>
                                        <div class="row">
                                            <label for="txtDisponibilidad" class="col-sm-2 col-form-label form-control-sm">Disponibilidad para viajar:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtDisponibilidad" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtA" class="col-sm-1 col-form-label form-control-sm">A:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtA" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div> 
                                        <br/>
                                        <div class="table-responsive">
                                            <asp:UpdatePanel ID="udpCompetencia" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grwCompetencia" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_com"
                                                        CssClass="display table table-sm" GridLines="None">
                                                        <Columns>
                                                            <asp:BoundField DataField="tipo_com" HeaderText="TIPO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="grado_com" HeaderText="GRADO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="nombre_com" HeaderText="COMPETENCIA" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="descripcion_com" HeaderText="DESCRIPCION" ItemStyle-Width="10%" ItemStyle-Wrap="false" />                                                                                                                                                                
                                                        </Columns>
                                                    </asp:GridView>                                
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br/>
                                        </div>
                                    </div>
                                    <div class="card-header">VI. PROGRAMA DE TRABAJO DEL PUESTO IDENTIFICADO</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtJornada" class="col-sm-2 col-form-label form-control-sm">Jornada de trabajo (N° Horas):</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtJornada" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtHorario" class="col-sm-1 col-form-label form-control-sm">Horario de trabajo:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtHorario" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-header">VII. ACCESOS DEL CAMPUS VIRTUAL PARA EL PUESTO IDENTIFICADO</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtDeno" class="col-sm-2 col-form-label form-control-sm">Denominación del Rol:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtDeno" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtProceso" class="col-sm-1 col-form-label form-control-sm">Proceso:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtProceso" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>                                        
                                        <div class="row">
                                            <label for="txtActividad" class="col-sm-2 col-form-label form-control-sm">Actividad:</label>
                                            <div class="col-sm-7">
                                                <ul id="myUL">
                                                    <li><span class="caret">Beverages</span>
                                                      <ul class="nested">
                                                        <li>Water</li>
                                                        <li>Coffee</li>
                                                        <li><span class="caret">Tea</span>
                                                          <ul class="nested">
                                                            <li>Black Tea</li>
                                                            <li>White Tea</li>
                                                            <li><span class="caret">Green Tea</span>
                                                              <ul class="nested">
                                                                <li>Sencha</li>
                                                                <li>Gyokuro</li>
                                                                <li>Matcha</li>
                                                                <li>Pi Lo Chun</li>
                                                              </ul>
                                                            </li>
                                                          </ul>
                                                        </li>  
                                                      </ul>
                                                    </li>
                                                  </ul>
                                            </div>                                         
                                        </div>
                                    </div>
                                    <div class="card-header">VIII. RECURSOS REQUERIDOS DEL PUESTO IDENTIFICADO</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtAT" class="col-sm-2 col-form-label form-control-sm">Ambiente de trabajo:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtAT" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>
                                        <div class="row">
                                            <label for="txtEG" class="col-sm-2 col-form-label form-control-sm">Equipo General:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtEG" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>
                                        <div class="row">
                                            <label for="txtEPP" class="col-sm-2 col-form-label form-control-sm">EPP:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtEPP" ReadOnly="true" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                            </div>                                         
                                        </div>
                                    </div>
                                    <div class="card-header">IX. RECOMENDACIONES DE SST ASOCIADAS AL PUESTO IDENTIFICADO</div>
                                    <div class="card-body">
                                        <div class="table-responsive">
                                            <div id="loading-recomendacion" class="loading oculto">
                                                <img src="img/loading.gif">
                                            </div> 
                                            <asp:UpdatePanel ID="udpRecomendacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grwRecomendacion" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_rec"
                                                        CssClass="display table table-sm" GridLines="None">
                                                        <Columns>
                                                            <asp:BoundField DataField="clasificacion" HeaderText="CLASIFICACIÓN DE PELIGRO" />
                                                            <asp:BoundField DataField="descripcion" HeaderText="DESCRIPCIÓN DEL PELIGRO" />
                                                            <asp:BoundField DataField="recomendacion" HeaderText="RECOMENDACIÓN" />  
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br/>
                                        </div>
                                    </div>
                                    <div class="card-footer" style="text-align: center;">
                                        <asp:LinkButton ID="btnSalirFicha" runat="server" CssClass="btn btn-accion btn-rojo">
                                            <i class="fa fa-sign-out-alt"></i>
                                            <span class="text">Salir</span>
                                        </asp:LinkButton>                                                 
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br/>
                        <br/>
                    </div>                    
                </div>
            </div>            
        </div>
        <!-- Modal -->
        <div id="mdlBuscarAlumno" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">ACCESOS ASIGNADOS</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div> 
                    <div class="modal-body">
                        <ul id="myUL">
                            <li><span class="caret">Beverages</span>
                              <ul class="nested">
                                <li>Water</li>
                                <li>Coffee</li>
                                <li><span class="caret">Tea</span>
                                  <ul class="nested">
                                    <li>Black Tea</li>
                                    <li>White Tea</li>
                                    <li><span class="caret">Green Tea</span>
                                      <ul class="nested">
                                        <li>Sencha</li>
                                        <li>Gyokuro</li>
                                        <li>Matcha</li>
                                        <li>Pi Lo Chun</li>
                                      </ul>
                                    </li>
                                  </ul>
                                </li>  
                              </ul>
                            </li>
                          </ul>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal2 -->
        <div id="mdlObservacion" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">REGISTRE SU OBSERVACIÓN</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div> 
                    <div class="modal-body">
                        </br>
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtObservacion" runat="server" MaxLength="800" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="7"/>
                        </div>
                        </br>
                        <div class="col-sm-12" style="text-align: center;">
                            <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                OnClientClick="return alertConfirm(this, event, '¿Desea registrar la observación?', 'warning');">
                                <i class="fa fa-save"></i>
                                <span class="text">Guardar</span>
                            </asp:LinkButton>                                                 
                        </div>
                        </br>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal3 -->
        <div id="mdlMensaje" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">MENSAJE DE LA OBSERVACIÓN</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div> 
                    <div class="modal-body">
                        </br>
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtMensaje" ReadOnly="true" runat="server" MaxLength="800" CssClass="form-control form-control-sm" AutoComplete="off" TextMode="MultiLine" Rows="7">El motivo u observación es... Metus fames pretium congue varius quisque risus accumsan at bibendum venenatis arcu tempor, viverra dictumst taciti odio justo rutrum cum tortor praesent ullamcorper sed. Nascetur consequat laoreet cum montes lectus sociosqu porta sodales, natoque nunc congue accumsan class quisque metus eleifend, gravida ac mollis malesuada molestie sociis rutrum ligula laoreet dapibus erat, vestibulum hendrerit etiam donec.
                            </asp:TextBox>  
                        </div>
                        </br>
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
        
        var toggler = document.getElementsByClassName("caret");
var i;

for (i = 0; i < toggler.length; i++) {
  toggler[i].addEventListener("click", function() {
    this.parentElement.querySelector(".nested").classList.toggle("active");
    this.classList.toggle("caret-down");
  });
}
    </script>
</body>
</html>