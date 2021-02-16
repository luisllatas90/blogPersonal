<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroAdeudos.aspx.vb" Inherits="GraduacionTitulacion_Deudas_frmRegistroAdeudos" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Registro de Adeudos</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../../../Alumni/css/datatables/jquery.dataTables.min.css">   
    <link rel="stylesheet" href="../../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">   
    <link rel="stylesheet" href="../../../Alumni/css/sweetalert/sweetalert2.min.css"> 
    
    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../../../Alumni/css/estilos.css">    

    <!-- Scripts externos -->
    <script src="../../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../../../Alumni/js/popper.js"></script>    
    <script src="../../../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../../../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../../../Alumni/js/datatables/jquery.dataTables.min.js?1"></script> 
    <script src="../../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>    
    
    <!-- Scripts propios -->
    <script src="../../../Alumni/js/funciones.js"></script>
    
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
            
            /*Fechas*/
            var date_FechaAdeudo=$('input[id="txtFechaAdeudo"]'); //our date input has the name "date"
            var date_FechaDevuelto=$('input[id="txtFechaDevuelto"]'); //our date input has the name "date"
            var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
            var options={
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_FechaAdeudo.datepicker(options);
            date_FechaDevuelto.datepicker(options);
        }   

        function udpFiltrosAlumnoUpdate(){
            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });             
        }

        function udpListaAlumnoUpdate() { 
            formatoGrillaAlumno();
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

        function formatoGrillaAlumno(){
            // Setup - add a text input to each footer cell
            $('#grwListaAlumno thead tr').clone(true).appendTo( '#grwListaAlumno thead' );
            $('#grwListaAlumno thead tr:eq(1) th').each( function (i) {
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
        
            var table = $('#grwListaAlumno').DataTable( {
                orderCellsTop: true,                
                fixedHeader: true ,
                order: [[ 2, "asc" ]]
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

                case 'ListarAlumno':
                    $loadingGif = $('#loading-listaAlumno');
                    $elemento = $('#udpListaAlumno');
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
            }
        }

        function closeModal(elemento) {
            switch (elemento) { 
                case 'BuscarAlumno':
                    $('#mdlBuscarAlumno').modal('hide');                   
                    break;                  
            }            
        }        
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmRegistroAdeudos" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">REGISTRO DE ADEUDOS</div>
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
                                            <label for="cmbAreaFiltro" class="col-sm-2 col-form-label form-control-sm">Área:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbAreaFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>
                                            </div>                                             
                                            <label for="cmbEstadoFiltro" class="col-sm-1 col-form-label form-control-sm">Estado:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbEstadoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>
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
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar un nuevo adeudo?', 'warning');">
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
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_ade"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="fechaDeuda_ade" HeaderText="FECHA" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="motivo_ade" HeaderText="MOTIVO" ItemStyle-Width="25%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="alumno" HeaderText="ESTUDIANTE" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="celular" HeaderText="CELULAR" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="telefono" HeaderText="TELÉFONO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="email_principal" HeaderText="EMAIL" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="deuda" HeaderText="DEUDA" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="estado_adeudos" HeaderText="ESTADO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />                                                                                
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar adeudo"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnGenerarDeuda" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="GenerarDeuda" 
                                                    CssClass="btn btn-warning btn-sm" 
                                                    ToolTip="Generar deuda"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea generar deuda al registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-dollar-sign"></i></span>
                                                </asp:LinkButton>                                                 
                                                <asp:LinkButton ID="btnDevolver" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Devolver" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Devolver"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea devolver el registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-check-circle"></i></span>
                                                </asp:LinkButton>                                               
                                                <!-- <asp:LinkButton ID="btnEliminar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Eliminar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Eliminar adeudo"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el registro seleccionado?', 'error');">
                                                    <span><i class="fa fa-trash"></i></span>
                                                </asp:LinkButton> -->
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
                                    <div class="card-header">Estudiante</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <asp:HiddenField ID="txtCodigoAlu" runat="server" />
                                            <label for="txtAlumno" class="col-sm-2 col-form-label form-control-sm">Estudiante:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtCodigoUniversitario" runat="server" MaxLength="50" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtAlumno" runat="server" MaxLength="500" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>  
                                            <div class="col-sm-2">
                                                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-accion btn-celeste">
                                                    <i class="fa fa-search-plus"></i>
                                                    <span class="text">Buscar</span>
                                                </asp:LinkButton> 
                                            </div>                                           
                                        </div>                                                                                                                                                                                                                                       
                                    </div>   
                                </div>
                                <br/>
                                <div class="card">
                                    <div class="card-header">Adeudo</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="cmbArea" class="col-sm-2 col-form-label form-control-sm">Área:</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="cmbArea" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>
                                            </div>                                             
                                        </div>
                                        <div class="row">
                                            <label for="txtMotivo" class="col-sm-2 col-form-label form-control-sm">Motivo:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtMotivo" runat="server" TextMode="MultiLine" Rows="2" MaxLength="500" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div> 
                                        </div> 
                                        <div class="row">
                                            <label for="txtFechaAdeudo" class="col-sm-2 col-form-label form-control-sm">Fecha:</label>
                                            <div class="input-group col-sm-2">
                                                <asp:TextBox ID="txtFechaAdeudo" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>                                                
                                            </div>
                                            <label for="txtMonto" class="col-sm-2 col-form-label form-control-sm">Monto referencial:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtMonto" runat="server" MaxLength="500" CssClass="form-control form-control-sm" placeholder="0.00" style="text-align: right !important;"
                                                    onkeypress="javascript:return soloNumerosDecimal(event)" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="cmbEstado" class="col-sm-2 col-form-label form-control-sm">Estado:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbEstado" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" Enabled="false" AutoComplete="off"></asp:DropDownList>
                                            </div>
                                            <label ID="lblGeneroDeuda" runat="server" for="chkGeneroDeuda" class="col-sm-2 col-form-label form-control-sm">Generó deuda:</label>
                                            <div class="col-sm-1">                                                
                                                <asp:CheckBox ID="chkGeneroDeuda" AutoPostBack="True" runat="server" onclick="return false;"/>
                                            </div> 
                                        </div> 
                                    </div>
                                </div>
                                <br/>
                                <div class="card">
                                    <div class="card-header">Devolución</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtFechaDevuelto" class="col-sm-2 col-form-label form-control-sm">Fecha:</label>
                                            <div class="input-group col-sm-2">
                                                <asp:TextBox ID="txtFechaDevuelto" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>                                                
                                            </div> 
                                        </div> 
                                        <div class="row">
                                            <label for="txtComentario" class="col-sm-2 col-form-label form-control-sm">Comentario:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Rows="2" MaxLength="500" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr/>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center;">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar el motivo de adeudo?', 'warning');">
                                            <i class="fa fa-save"></i>
                                            <span class="text">Guardar</span>
                                        </asp:LinkButton>    
                                        <asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-accion btn-danger">
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
            </div>
        </div>

        <!-- Modal Busqueda de Alumnos -->
        <div id="mdlBuscarAlumno" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">BUSCAR ESTUDIANTES</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div> 
                    <div class="modal-body">
                        <div class="container-fluid">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a href="#listaAlumno" id="listaAlumno-tab" class="nav-link active" data-toggle="tab" role="tab"
                                        aria-controls="listaAlumno" aria-selected="true">Listado</a>
                                </li>
                            </ul>
                            <div class="tab-content" id="contentTabsAlumno">
                                <div class="tab-pane show active" id="listaAlumno" role="tabpanel" aria-labelledby="listaAlumno-tab">
                                    <div class="panel-cabecera">
                                        <asp:UpdatePanel ID="udpFiltrosAlumno" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="card">
                                                    <div class="card-header">Filtros de Búsqueda</div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <label for="cmbNivelFiltro" class="col-sm-2 col-form-label form-control-sm">Nivel:</label>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="cmbNivelFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>                                    
                                                            </div>  
                                                            <label for="cmbFacultadFiltro" class="col-sm-2 col-form-label form-control-sm">Facultad:</label>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="cmbFacultadFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>                                    
                                                            </div>                                             
                                                        </div>
                                                        <div class="row">
                                                            <label for="cmbCarreraFiltro" class="col-sm-2 col-form-label form-control-sm">Carrera:</label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="cmbCarreraFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>                                    
                                                            </div>                                            
                                                        </div>  
                                                        <div class="row">
                                                            <label for="txtNombreFiltro" class="col-sm-2 form-control-sm">Nombre / DNI:</label>
                                                            <div class="col-sm-8">                                                                                
                                                                <asp:TextBox ID="txtNombreFiltro" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                                            </div>                                           
                                                        </div>
                                                        <hr/>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <asp:LinkButton ID="btnListarAlumno" runat="server" CssClass="btn btn-accion btn-celeste">
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
                                    <br/>
                                    <div class="table-responsive">
                                        <div id="loading-listaAlumno" class="loading oculto">
                                            <img src="../../../Alumni/img/loading.gif">
                                        </div>                 
                                        <asp:UpdatePanel ID="udpListaAlumno" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:GridView ID="grwListaAlumno" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_alu, codigoUniver_Alu, nombre_completo"
                                                    CssClass="display table table-sm" GridLines="None">
                                                    <Columns>                                                                                         
                                                        <asp:TemplateField HeaderText="SEL." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnSeleccionar" runat="server" 
                                                                    CommandName="Seleccionar" 
                                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                                    CssClass="btn btn-primary btn-sm" 
                                                                    ToolTip="Seleccionar alumno"
                                                                    OnClientClick="return alertConfirm(this, event, '¿Desea enviar enviar el estudiante seleccionado?', 'warning');">
                                                                    <span><i class="fa fa-check-circle"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        <asp:BoundField DataField="codigoUniver_Alu" HeaderText="CÓD. UNIV." ItemStyle-Width="15%" ItemStyle-Wrap="false"/>
                                                        <asp:BoundField DataField="nombre_completo" HeaderText="APELLIDOS Y NOMBRES" ItemStyle-Width="25%" ItemStyle-Wrap="false"/>
                                                        <asp:BoundField DataField="tipoDocIdent_Alu" HeaderText="DOC. IDENT." ItemStyle-Width="10%" ItemStyle-Wrap="false"/>
                                                        <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="NRO. DOC." ItemStyle-Width="10%" ItemStyle-Wrap="false"/>
                                                        <asp:BoundField DataField="nivel" HeaderText="NIVEL" ItemStyle-Width="15%" ItemStyle-Wrap="false"/>
                                                        <asp:BoundField DataField="modalidad" HeaderText="MODALIDAD" ItemStyle-Width="15%" ItemStyle-Wrap="false"/>
                                                        <asp:BoundField DataField="facultad" HeaderText="FACULTAD" ItemStyle-Width="15%" ItemStyle-Wrap="false"/>
                                                        <asp:BoundField DataField="escuela_profesional" HeaderText="CARRERA" ItemStyle-Width="25%" ItemStyle-Wrap="false"/>
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
                    break;

                case 'btnListarAlumno':
                    AlternarLoading(false, 'ListarAlumno');                    
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
                    break;

                case 'btnListarAlumno':
                    AlternarLoading(true, 'ListarAlumno');                    
                    break;                       
                            
            }
        });    
    </script>    
</body>
</html>
