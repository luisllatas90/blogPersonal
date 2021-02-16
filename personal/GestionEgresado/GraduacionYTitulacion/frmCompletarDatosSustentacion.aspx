<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCompletarDatosSustentacion.aspx.vb" Inherits="GestionEgresado_GraduacionYTitulacion_frmCompletarDatosSustentacion" %>

<!DOCTYPE html>
<html lang="es">
<head>     
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Completar Datos de Sustentacioón</title>
    
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../../Alumni/css/datatables/jquery.dataTables.min.css">      
    <link rel="stylesheet" href="../../Alumni/css/sweetalert/sweetalert2.min.css"> 
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">    

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../../Alumni/css/estilos.css">

    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../../Alumni/js/popper.js"></script>    
    <script src="../../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../../Alumni/js/datatables/jquery.dataTables.min.js?1"></script> 
    <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>    

    <!-- Scripts propios -->
    <script src="../../Alumni/js/funciones.js?1"></script>
    
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
            var date_resolucion=$('input[id="txtFechaResolucion"]'); //our date input has the name "date"
            var date_acta=$('input[id="txtFechaActa"]'); //our date input has the name "date"
            var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
            var options={
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_resolucion.datepicker(options);
            date_acta.datepicker(options);            
        }

        function udpFiltrosDocenteUpdate(){
            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });             
        }

        function udpListaDocenteUpdate() { 
            formatoGrillaDocente();
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

        function formatoGrillaDocente(){
            // Setup - add a text input to each footer cell
            $('#grwListaDocente thead tr').clone(true).appendTo( '#grwListaDocente thead' );
            $('#grwListaDocente thead tr:eq(1) th').each( function (i) {
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
        
            var table = $('#grwListaDocente').DataTable( {
                orderCellsTop: true,                
                fixedHeader: true ,
                order: [[ 1, "asc" ]]
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

                case 'ListarDocente':
                    $loadingGif = $('#loading-listaDocente');
                    $elemento = $('#udpListaDocente');
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

    <form id="frmCompletarDatosSustentacion" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">COMPLETAR DATOS DE SUSTENTACION A TRÁMITES DE TITULACIÓN</div>                
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
                                            <label for="cmbTipoFiltro" class="col-sm-1 col-form-label form-control-sm">Estado:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="N">PENDIENTE</asp:ListItem>
                                                    <asp:ListItem Value="S">COMPLETADO</asp:ListItem>                                                                                                       
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
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_alu, codigoUniver_alu, bachiller, codigo_tes"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="glosaCorrelativo_trl" HeaderText="NRO.TRÁMITE" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                       <asp:BoundField DataField="fechaReg_trl" HeaderText="FECHA" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                       <asp:BoundField DataField="nombre_cpf" HeaderText="ESCUELA" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                       <asp:BoundField DataField="bachiller" HeaderText="EGRESADO" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                       <asp:BoundField DataField="estado" HeaderText="ESTADO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                       <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar datos de sustentación"
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
                                    <div class="card-header">Datos del Egresado / Tesis</div>
                                    <div class="card-body">                                        
                                        <div class="row">
                                            <label for="txtCodigo" class="col-sm-2 col-form-label form-control-sm">Código:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtCodigo" runat="server" ReadOnly="true" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">                                            
                                            <label for="txtEgresado" class="col-sm-2 col-form-label form-control-sm">Egresado:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtEgresado" runat="server" ReadOnly="true" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                                                                             
                                        </div>
                                    </div>
                                    <br />
                                    <div class="card-header">Datos de la Resolución de Sustentación</div>
                                    <div class="card-body">
                                         <div class="row">
                                             <label for="txtNumeroResolucion" class="col-sm-2 col-form-label form-control-sm">Número:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtNumeroResolucion" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtFechaResolucion" class="col-sm-1 col-form-label form-control-sm">Fecha:</label>
                                            <div class="input-group col-sm-2">
                                                <asp:TextBox ID="txtFechaResolucion" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>                                                
                                            </div>                                                
                                        </div>
                                    </div>
                                    <br />
                                    <div class="card-header">Datos del Acta de Sustentación</div>
                                    <div class="card-body">  
                                        <div class="row">
                                            <label for="txtNumeroActa" class="col-sm-2 col-form-label form-control-sm">Número:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtNumeroActa" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtFechaActa" class="col-sm-1 col-form-label form-control-sm">Fecha:</label>
                                            <div class="input-group col-sm-2">
                                                <asp:TextBox ID="txtFechaActa" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>                                                
                                            </div>
                                        </div>                                                                             
                                        <div class="row">
                                            <label for="txtNota" class="col-sm-2 col-form-label form-control-sm">Nota:</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtNota" runat="server" MaxLength="2" onkeypress="javascript:return soloNumeros(event)" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="cmbCalificativo" class="col-sm-1 col-form-label form-control-sm">Calificativo:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbCalificativo" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>
                                            </div>            
                                        </div> 
                                     </div>
                                     <br /> 
                                     <div class="card-header">Jurado de la Sustentación de Tesis</div>
                                     <div class="card-body"> 
                                         <div class="row">
                                            <asp:HiddenField ID="txtCodigoPresidente" runat="server" />
                                            <asp:HiddenField ID="txtCodigoJuradoPresidente" runat="server" />
                                            <label for="txtPresidente" class="col-sm-2 col-form-label form-control-sm">Presidente:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtPresidente" runat="server" ReadOnly="true" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:LinkButton ID="btnPresidente" runat="server" CssClass="btn btn-sm btn-success">
                                                    <i class="fa fa-search"></i>
                                                </asp:LinkButton>
                                            </div>
                                          </div>
                                          <div class="row">
                                            <asp:HiddenField ID="txtCodigoSecretario" runat="server" />
                                            <asp:HiddenField ID="txtCodigoJuradoSecretario" runat="server" />
                                            <label for="txtSecretario" class="col-sm-2 col-form-label form-control-sm">Secretario:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtSecretario" runat="server" ReadOnly="true" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:LinkButton ID="btnSecretario" runat="server" CssClass="btn btn-sm btn-success">
                                                    <i class="fa fa-search"></i>
                                                </asp:LinkButton>
                                            </div>
                                          </div>
                                          <div class="row">
                                            <asp:HiddenField ID="txtCodigoVocal" runat="server" />
                                            <asp:HiddenField ID="txtCodigoJuradoVocal" runat="server" />
                                            <label for="txtVocal" class="col-sm-2 col-form-label form-control-sm">Vocal:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtVocal" runat="server" ReadOnly="true" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:LinkButton ID="btnVocal" runat="server" CssClass="btn btn-sm btn-success">
                                                    <i class="fa fa-search"></i>
                                                </asp:LinkButton>
                                            </div>                                                 
                                        </div>                                     
                                     </div>
                                     <br />                                    
                                    <div class="card-footer" style="text-align: center;">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar los datos de sustentación?', 'warning');">
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
        
        <!-- Modal Busqueda de Docentes -->
        <div id="mdlBuscarDocente" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">BUSCAR DOCENTES</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a href="#listaDocente" id="listaDocente-tab" class="nav-link active" data-toggle="tab" role="tab"
                                        aria-controls="listaDocente" aria-selected="true">Listado</a>
                                </li>
                            </ul>
                            <div class="tab-content" id="contentTabsDocente">
                                <div class="tab-pane show active" id="listaDocente" role="tabpanel" aria-labelledby="listaDocente-tab">
                                    <div class="panel-cabecera">
                                        <asp:UpdatePanel ID="udpFiltrosDocente" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="card">
                                                    <div class="card-header">Filtros de Búsqueda</div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <label for="cmbCicloFiltro" class="col-sm-1 col-form-label form-control-sm">Ciclo:</label>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="cmbCicloFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>                                    
                                                            </div>
                                                        </div>
                                                        <hr/>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <asp:LinkButton ID="btnListarDocente" runat="server" CssClass="btn btn-accion btn-celeste">
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
                                        <div id="loading-listaDocente" class="loading oculto">
                                            <img src="../../Alumni/img/loading.gif">
                                        </div>
                                        <asp:UpdatePanel ID="udpListaDocente" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:GridView ID="grwListaDocente" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_per, nombre_per"
                                                    CssClass="display table table-sm" GridLines="None">
                                                    <Columns>                                                                                         
                                                        <asp:TemplateField HeaderText="SEL." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnSeleccionar" runat="server" 
                                                                    CommandName="Seleccionar" 
                                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                                    CssClass="btn btn-primary btn-sm" 
                                                                    ToolTip="Seleccionar docente"
                                                                    OnClientClick="return alertConfirm(this, event, '¿Desea enviar el docente seleccionado?', 'warning');">
                                                                    <span><i class="fa fa-check-circle"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                         
                                                        <asp:BoundField DataField="nombre_per" HeaderText="APELLIDOS Y NOMBRES" ItemStyle-Width="25%" ItemStyle-Wrap="false"/>                                                        
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
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function(sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            switch (controlId) {
                case 'btnListar':
                    AlternarLoading(false, 'Lista');
                    break;

                case 'btnGuardar':
                    AlternarLoading(false, 'Registro');
                    break;

                case 'btnListarDocente':
                    AlternarLoading(false, 'ListarDocente');                    
                    break;                     
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
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

                case 'btnListarDocente':
                    AlternarLoading(true, 'ListarDocente');                    
                    break;                    
            }
        });    
    </script>    
</body>
</html>
