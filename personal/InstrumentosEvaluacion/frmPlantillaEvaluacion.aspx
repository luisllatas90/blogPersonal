<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPlantillaEvaluacion.aspx.vb" Inherits="InstrumentosEvaluacion_frmPlantillaEvaluacion" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Plantillas de Evaluación</title>

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
            actualizarSummernote();

             /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });        
        }

        function udpListaPreguntasUpdate() {
            formatoGrillaPreguntas();
        }

        function udpRegistroPreguntasUpdate() { 
            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });        
        }

        function actualizarSummernote(){
            /*Editor de Texto*/
            $('.summernote').summernote('destroy');            
            $('.summernote').summernote({
                placeholder: 'Redacta tus indicaciones...',
                tabsize: 2,
                height: 150,
                focus: true
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
        
        /* Dar formato a la grilla. */
        function formatoGrillaPreguntas(){
            // Setup - add a text input to each footer cell
            $('#grwListaPreguntas thead tr').clone(true).appendTo( '#grwListaPreguntas thead' );
            $('#grwListaPreguntas thead tr:eq(1) th').each( function (i) {
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
        
            $('#grwListaPreguntas').DataTable( {
                orderCellsTop: true,
                order: [[ 3, "asc" ]],
                pageLength: 50
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
            if (ctl.id == 'btnGuardar') {
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
                estadoTabPregunta('D');

            }else if (tabActivo == 'registro-tab'){
                //HABILITAR
                estadoTabRegistro('H');

                //DESHABILITAR
                estadoTabListado('D');
                estadoTabPregunta('D');
            }else if (tabActivo == 'pregunta-tab'){
                //HABILITAR
                estadoTabPregunta('H');

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

        function estadoTabPregunta(estado) {
            if (estado == 'H') {
                $("#pregunta-tab").removeClass("disabled");
                $("#pregunta-tab").addClass("active");
                $("#pregunta").addClass("show");
                $("#pregunta").addClass("active");
            } else {
                $("#pregunta-tab").removeClass("active");
                $("#pregunta-tab").addClass("disabled");
                $("#pregunta").removeClass("show");
                $("#pregunta").removeClass("active");
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

        /*Permite codificar contenido html*/
        function makeSafe() {            
            //var contenido = window.escape($('#divMensaje').summernote('code'));
            var contenido = encodeURI($('#divIndicaciones').summernote('code'));                           
            $("#<%=txtIndicaciones.ClientID%>").val(contenido);
        };  

        function setCodigoIndicaciones(contenido){
            $('#divIndicaciones').summernote('code', contenido);
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

    <form id="frmPlantillaEvaluacion" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">PLANTILLAS DE EVALUACIÓN</div>
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
                    <a href="#pregunta" id="pregunta-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="pregunta" aria-selected="false">Preguntas</a>
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
                                            <label for="cmbTipoFiltro" class="col-sm-2 col-form-label form-control-sm">Tipo:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbTipoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="DD">EVALUACIÓN DOCENTE - AUTOEVALUACIÓN</asp:ListItem>
                                                    <asp:ListItem Value="DE">EVALUACIÓN DOCENTE - ESTUDIANTES</asp:ListItem>
                                                    <asp:ListItem Value="DO">EVALUACIÓN DOCENTE - OBSERVADOR</asp:ListItem>
                                                    <asp:ListItem Value="DS">EVALUACIÓN DOCENTE - DIRECTOR DE ESCUELA</asp:ListItem>
                                                    <asp:ListItem Value="DB">ENCUESTA DE SATISFACCIÓN</asp:ListItem>
                                                    <asp:ListItem Value="DI">ENCUESTA QUEREMOS ESCUCHARTE</asp:ListItem>
                                                    <asp:ListItem Value="DT">TALLERES DE FORMACIÓN COMPLEMENTARIA</asp:ListItem>
                                                    <asp:ListItem Value="DZ">MONITOREO DE LA SESIÓN DE CLASE VIRTUAL</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label for="cmbEstadoFiltro" class="col-sm-2 col-form-label form-control-sm">Estado:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbEstadoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="A">ACTIVO</asp:ListItem>
                                                    <asp:ListItem Value="I">INACTIVO</asp:ListItem>                                                    
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
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_ple"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="titulo_ple" HeaderText="TÍTULO" ItemStyle-Width="50%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="tipo_ple" HeaderText="TIPO" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="estado_ple" HeaderText="ESTADO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar plantilla"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnPregunta" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Pregunta" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Grupos y Preguntas"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar los registros de grupos y preguntas?', 'warning');">
                                                    <span><i class="fas fa-list-alt"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Eliminar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Eliminar plantilla"
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
                                    <div class="card-header">Datos de la Plantilla de Evaluación</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="cmbTipo" class="col-sm-2 col-form-label form-control-sm">Tipo:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbTipo" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="DD">EVALUACIÓN DOCENTE - AUTOEVALUACIÓN</asp:ListItem>
                                                    <asp:ListItem Value="DE">EVALUACIÓN DOCENTE - ESTUDIANTES</asp:ListItem>
                                                    <asp:ListItem Value="DO">EVALUACIÓN DOCENTE - OBSERVADOR</asp:ListItem>
                                                    <asp:ListItem Value="DS">EVALUACIÓN DOCENTE - DIRECTOR DE ESCUELA</asp:ListItem>
                                                    <asp:ListItem Value="DB">ENCUESTA DE SATISFACCIÓN</asp:ListItem>
                                                    <asp:ListItem Value="DI">ENCUESTA QUEREMOS ESCUCHARTE</asp:ListItem>
                                                    <asp:ListItem Value="DT">TALLERES DE FORMACIÓN COMPLEMENTARIA</asp:ListItem>
                                                    <asp:ListItem Value="DZ">MONITOREO DE LA SESIÓN DE CLASE VIRTUAL</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label for="cmbEstado" class="col-sm-2 col-form-label form-control-sm">Estado:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbEstado" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="A">ACTIVO</asp:ListItem>
                                                    <asp:ListItem Value="I">INACTIVO</asp:ListItem>                                                    
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtTitulo" class="col-sm-2 col-form-label form-control-sm">Título:</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtTitulo" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>                                        
                                        <div class="row">
                                            <label for="divIndicaciones" class="col-sm-2 col-form-label form-control-sm">Indicaciones:</label>
                                            <div class="col-sm-8">
                                                <asp:HiddenField ID="txtIndicaciones" runat="server" />
                                                <div id="divIndicaciones" class="summernote"></div>
                                            </div>                                            
                                        </div>
                                        <div class="row">
                                            <label for="cmbEscalaEvaluacion" class="col-sm-2 form-control-sm">Esc. Evaluación:</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="cmbEscalaEvaluacion" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="1">ESCALA DE LIKERT (1 - 5)</asp:ListItem>                                                                                                       
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="cmbEscalaResultado" class="col-sm-2 form-control-sm">Esc. Resultados:</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="cmbEscalaResultado" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="1">EVALUACIÓN DE RESULTADOS (1 - 5)</asp:ListItem>                                                                                                       
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer" style="text-align: center;">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar la plantilla?', 'warning');">
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
                <div class="tab-pane" id="pregunta" role="tabpanel" aria-labelledby="pregunta-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpListaPreguntas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">REGISTRO DE GRUPOS Y PREGUNTAS</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:LinkButton ID="btnNuevoPregunta" runat="server" CssClass="btn btn-accion btn-verde">
                                                    <i class="fa fa-plus-circle"></i>
                                                    <span class="text">Nuevo</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnSalirPregunta" runat="server" CssClass="btn btn-accion btn-rojo">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
                                                </asp:LinkButton> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br/>
                                <div class="table-responsive">
                                    <asp:GridView ID="grwListaPreguntas" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames=""
                                        CssClass="display table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="tipo_enunciado" HeaderText="TIPO ENUNC." ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="numeracion" HeaderText="NUMERACIÓN" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="enunciado" HeaderText="ENUNCIADO" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="orden" HeaderText="ORDEN" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="tipo_pregunta" HeaderText="TIPO PREG." ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="escala" HeaderText="ESCALA" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                            <asp:TemplateField HeaderText="PREG. SI/NO">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbSiNo" Checked='<%# Convert.ToBoolean(Eval("pregunta_sino")) %>' runat="server" OnClick="return false;"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="grupo" HeaderText="SUPERIOR" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                            <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                        CommandName="Editar" 
                                                        CssClass="btn btn-primary btn-sm" 
                                                        ToolTip="Editar Detalle de Escala"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                        <span><i class="fa fa-pen"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminar" runat="server" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                        CommandName="Eliminar" 
                                                        CssClass="btn btn-danger btn-sm" 
                                                        ToolTip="Eliminar Detalle de Escala"
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

        <!-- Modal Registro de Pregunta -->
        <div id="mdlRegistroPreguntas" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">REGISTRO DE PREGUNTAS</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpRegistroPreguntas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="cmbTipoEnunciado" class="col-sm-2 form-control-sm">Tipo Enunciado:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoEnunciado" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="N">GRUPO</asp:ListItem>
                                                    <asp:ListItem Value="S">PREGUNTA</asp:ListItem>                                                    
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="cmbGrupo" class="col-sm-2 form-control-sm">Grupo:</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="cmbGrupo" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="1">I. Cumplimiento de obligaciones</asp:ListItem>
                                                    <asp:ListItem Value="2">II. Evaluación</asp:ListItem>                                                    
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtNumeracion" class="col-sm-2 form-control-sm">Numeración:</label>
                                            <div class="col-sm-1">                                                                                
                                                <asp:TextBox ID="txtNumeracion" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div> 
                                            <label for="txtOrdenPregunta" class="col-sm-2 form-control-sm">Orden:</label>
                                            <div class="col-sm-1">                                                                                
                                                <asp:TextBox ID="txtOrdenPregunta" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="cmbTipoPregunta" class="col-sm-2 form-control-sm">Tipo Pregunta:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoPregunta" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="A">ABIERTA</asp:ListItem>
                                                    <asp:ListItem Value="C">CERRADA</asp:ListItem>                                                    
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtEnunciado" class="col-sm-2 form-control-sm">Enunciado:</label>
                                            <div class="col-sm-9">                                                                                
                                                <asp:TextBox ID="txtEnunciado" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>                                                                           
                                        <div class="row">
                                            <label for="cmbEscalaPregunta" class="col-sm-2 form-control-sm">Escala:</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="cmbEscalaPregunta" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="1">EVALUACIÓN DE RESULTADOS (1 - 5)</asp:ListItem>                                                                                                       
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3" style="text-align: right;">                                                
                                                <asp:CheckBox ID="chkPreguntaSiNo" AutoPostBack="True" runat="server"/>
                                                <label for="chkPreguntaSiNo" class="form-control-sm">Pregunta SI / NO</label>
                                            </div>
                                        </div>
                                        <div class="row"> 
                                            <label for="txtArchivoImagen" class="col-sm-2 col-form-label form-control-sm">Cargar Imagen:</label> 
                                            <div class="col-sm-9">
                                                <asp:FileUpload ID="txtArchivoImagen" runat="server" CssClass="form-control form-control-sm" />
                                            </div>
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: center;">
                                                <asp:LinkButton ID="btnGuardarPregunta" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea guardar el registro?', 'warning');">
                                                    <i class="fa fa-save"></i>
                                                    <span class="text">Guardar</span>
                                                </asp:LinkButton>                                                                                                                                                                 
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
