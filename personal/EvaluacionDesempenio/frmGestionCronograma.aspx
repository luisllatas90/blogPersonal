<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGestionCronograma.aspx.vb" Inherits="EvaluacionDesempenio_frmGestionCronograma" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />      
    <title>Gestión de Cronograma</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../Alumni/css/datatables/jquery.dataTables.min.css">      
    <link rel="stylesheet" href="../Alumni/css/sweetalert/sweetalert2.min.css"> 
    <link rel="stylesheet" href="../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
    
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
    <script src="../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="http://cdn.datatables.net/plug-ins/1.10.11/sorting/date-eu.js"></script>

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
            
            /*Datepicker*/
            var date_inicio=$('input[id="txtFechaInicio"]'); //our date input has the name "date"
            var date_fin=$('input[id="txtFechaFin"]'); //our date input has the name "date"
            var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
            var options={
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_inicio.datepicker(options);
            date_fin.datepicker(options);            
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
        
            $('#grwLista').DataTable( {
                orderCellsTop: true,
                columnDefs : [{targets: 0, type:"date-eu"}, {targets: 1, type:"date-eu"}],
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

    <form id="frmGestionCronograma" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">GESTIÓN DE CRONOGRAMA</div>
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
                                            <label for="cmbAnioFiltro" class="col-sm-2 col-form-label form-control-sm">Año:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbAnioFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="2021">2021</asp:ListItem>                                                    
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
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames=""
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="fecha_desde" HeaderText="DESDE" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="fecha_hasta" HeaderText="HASTA" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="actividad" HeaderText="ACTIVIDAD" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="responsable" HeaderText="RESPONSABLE" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
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
                <div class="tab-pane" id="registro" role="tabpanel" aria-labelledby="registro-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">Datos del Cronograma</div>
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
                                            <label for="cmbResponsable" class="col-sm-1 col-form-label form-control-sm">Resposable:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbResponsable" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="1">TRABAJADOR(A)</asp:ListItem>
                                                    <asp:ListItem Value="2">DIRECTOR(A) DE PERSONAL</asp:ListItem>
                                                    <asp:ListItem Value="3">JEFE INMEDIATO</asp:ListItem>
                                                    <asp:ListItem Value="4">DIRECTOR(A) DE DEPARTAMENTO</asp:ListItem>
                                                    <asp:ListItem Value="5">DIRECTOR DE ESCUELA</asp:ListItem>
                                                    <asp:ListItem Value="6">DOCENTE</asp:ListItem>
                                                    <asp:ListItem Value="7">ESTUDIANTE</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="cmbActividad" class="col-sm-2 col-form-label form-control-sm">Actividad:</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="cmbActividad" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="1">ACUERDO DE METAS Y COMPETENCIAS</asp:ListItem>
                                                    <asp:ListItem Value="2">FECHA LÍMITE PARA ENTREGA DE ACUERDOS</asp:ListItem>
                                                    <asp:ListItem Value="3">REVISIÓN DE AVANCE DE DESEMPEÑO</asp:ListItem>
                                                    <asp:ListItem Value="4">FECHA LÍMITE PARA REVISIÓN DE AVANCES</asp:ListItem>
                                                    <asp:ListItem Value="5">REVISIÓN FINAL DE EVALUACIÓN DE DESEMPEÑO</asp:ListItem>
                                                    <asp:ListItem Value="6">FECHA LÍMITE PARA LA ENTREGA DE RESULTADOS</asp:ListItem>
                                                    <asp:ListItem Value="7">LLENAR LA EVALUACIÓN VIRTUAL "COMPROMISO INSTITUCIONAL"</asp:ListItem>
                                                    <asp:ListItem Value="8">LLENAR LA EVALUACIÓN VIRTUAL "COMPETENCIAS GENERALES"</asp:ListItem>
                                                    <asp:ListItem Value="9">COORDINAR LA EJECUCIÓN DE LA OBSERVACIÓN EN CLASE</asp:ListItem>
                                                    <asp:ListItem Value="10">LLENAR LA EVALUACIÓN VIRTUAL "COMPETENCIAS ESPECÍFICAS"</asp:ListItem>
                                                    <asp:ListItem Value="11">FECHA LÍMITE DE EVALUACIÓN DE DESEMPEÑO DOCENTE</asp:ListItem>
                                                    <asp:ListItem Value="12">ANÁLISIS Y VALIDACIÓN DE RESULTADOS</asp:ListItem>
                                                    <asp:ListItem Value="13">DESCARGA Y FIRMAS CORRESPONDIENTES</asp:ListItem>
                                                    <asp:ListItem Value="14">FECHA DE LÍMITE DE ENTREGA DE EVALUACIÓN DE DESEMPEÑO A DIRECCIÓN DE PERSONAL</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtFechaInicio" class="col-sm-2 col-form-label form-control-sm">Desde:</label>
                                            <div class="input-group col-sm-3">
                                                <asp:TextBox ID="txtFechaInicio" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>                                                
                                            </div>
                                            <label for="txtFechaFin" class="col-sm-1 col-form-label form-control-sm">Hasta:</label>
                                            <div class="input-group col-sm-3">
                                                <asp:TextBox ID="txtFechaFin" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>                                                
                                            </div>                                            
                                        </div>                                        
                                    </div>
                                    <div class="card-footer" style="text-align: center;">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                            OnClientClick="return alertConfirm(this, event, '¿Desea realizar el registro del cronograma?', 'warning');">
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
