<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActividadEvento.aspx.vb" Inherits="Alumni_frmActividadEvento" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Actividad Evento</title>

    <!-- Estilos externos -->         
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">        
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="css/datatables/jquery.dataTables.min.css">   
    <link rel="stylesheet" href="../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
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
    <script src="../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>

    <!-- Scripts propios -->
    <script src="js/funciones.js?3"></script>

    <script type="text/javascript">              

        /* Scripts a ejecutar al actualizar los paneles.*/
        function udpAsistenciaUpdate() {
            var date_input=$('input[id="txtFechaAsistencia"]'); //our date input has the name "date"
            var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
            var options={
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_input.datepicker(options);
        }

        function udpActividadUpdate(){
            formatoGrillaActividad();
        }

        function udpRegistroUpdate() {
            $('#cmbTipo').selectpicker({
                size: 6,
            });

            $('#cmbServicioConcepto').selectpicker({
                size: 6,
            });
        }

        function udpListaUpdate(){
            formatoGrilla();
        }

        function udpParticipantesSMSUpdate(){
            formatoGrillaParticipantesSMS();
        }

        function udpRegistroProgramacionUpdate(){
            var date_input=$('input[id="txtFechaProgramacion"]'); //our date input has the name "date"
            var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
            var options={
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_input.datepicker(options);            
        }

        function udpListaProgramacionUpdate() {
            formatoGrillaProgramacion();
        }

        function udpVerInscritosUpdate() {
            formatoGrillaInscritos();
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

        function formatoGrillaActividad() {
            // Setup - add a text input to each footer cell
            $('#grwActividad thead tr').clone(true).appendTo( '#grwActividad thead' );
            $('#grwActividad thead tr:eq(1) th').each( function (i) {
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

            var table = $('#grwActividad').DataTable( {
                orderCellsTop: true
            } );            
        }

        function formatoGrillaParticipantesSMS(){
            // Setup - add a text input to each footer cell
            $('#grwParticipantesSMS thead tr').clone(true).appendTo( '#grwParticipantesSMS thead' );
            $('#grwParticipantesSMS thead tr:eq(1) th').each( function (i) {
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

            var table = $('#grwParticipantesSMS').DataTable( {
                orderCellsTop: true
            } );
        }

        function formatoGrillaProgramacion() {
            $('#grwListaProgramacion thead tr').clone(true).appendTo( '#grwListaProgramacion thead' );
            $('#grwListaProgramacion thead tr:eq(1) th').each( function (i) {
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

            var table = $('#grwListaProgramacion').DataTable( {
                orderCellsTop: true
            } );            
        }

        function formatoGrillaInscritos() {
            $('#gvListaVerInscritos thead tr').clone(true).appendTo( '#gvListaVerInscritos thead' );
            $('#gvListaVerInscritos thead tr:eq(1) th').each( function (i) {
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

            var table = $('#gvListaVerInscritos').DataTable( {
                orderCellsTop: true,
                "order": [[1, "asc"]]       
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

        /* Abrir y cerrar el modales. */
        function openModal(elemento) {
            switch (elemento) { 
                case 'Actividad':
                    $('#mdlRegistro').modal('show');                   
                    break;    
                    
                case 'Programacion':
                    $('#mdlProgramacion').modal('show');                   
                    break;                      
            }
        }

        function closeModal(elemento) {
            switch (elemento) { 
                case 'Actividad':
                    $('#mdlRegistro').modal('hide');                   
                    break;

                case 'Programacion':
                    $('#mdlProgramacion').modal('hide');                   
                    break;                                  
            }            
        }   

        /* Flujo de tabs de la página principal. */
        function flujoTabs(tabActivo) {                    
            if (tabActivo == 'actividad-tab') {     
                //HABILITAR  
                estadoTabActividad('H');            

                //DESHABILITAR 
                estadoTabListado('D');
                estadoTabEnvioSMS('D');
                estadoTabAsistencia('D');
                estadoTabInscripcion('D'); 
                estadoTabVerInscritos('D'); 

            }else if (tabActivo == 'listado-tab'){
                //HABILITAR             
                estadoTabListado('H');

                //DESHABILITAR 
                estadoTabActividad('D');
                estadoTabEnvioSMS('D');
                estadoTabAsistencia('D');
                estadoTabInscripcion('D');
                estadoTabVerInscritos('D'); 
                

            }else if (tabActivo == 'enviosms-tab'){
                //HABILITAR
                estadoTabEnvioSMS('H');

                //DESHABILITAR 
                estadoTabListado('D');
                estadoTabActividad('D');
                estadoTabAsistencia('D');
                estadoTabInscripcion('D');
                estadoTabVerInscritos('D'); 
                
            
            }else if (tabActivo == 'asistencia-tab'){
                //HABILITAR
                estadoTabAsistencia('H');

                //DESHABILITAR 
                estadoTabListado('D');
                estadoTabActividad('D');
                estadoTabEnvioSMS('D');
                estadoTabInscripcion('D');
                estadoTabVerInscritos('D'); 
                
            
            /// olluen 27/02/2020
            }else if (tabActivo == 'inscripcion-tab'){
                //HABILITAR
                estadoTabInscripcion('H');

                //DESHABILITAR 
                estadoTabAsistencia('D');
                estadoTabListado('D');
                estadoTabActividad('D');
                estadoTabEnvioSMS('D'); 
                estadoTabVerInscritos('D'); 
                
            } else if (tabActivo == 'verInscritos-tab'){
                //HABILITAR
                estadoTabVerInscritos('H');

                //DESHABILITAR 
                estadoTabAsistencia('D');
                estadoTabListado('D');
                estadoTabActividad('D');
                estadoTabEnvioSMS('D');  
                estadoTabInscripcion('D');
                
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

        function estadoTabActividad(estado) { 
            if (estado == 'H') {
                $("#actividad-tab").removeClass("disabled");
                $("#actividad-tab").addClass("active");
                $("#actividad").addClass("show");
                $("#actividad").addClass("active");
            } else {
                $("#actividad-tab").removeClass("active");
                $("#actividad-tab").addClass("disabled");
                $("#actividad").removeClass("show");
                $("#actividad").removeClass("active");
            }
        }

        function estadoTabEnvioSMS(estado) { 
            if (estado == 'H') {
                $("#enviosms-tab").removeClass("disabled");
                $("#enviosms-tab").addClass("active");
                $("#enviosms").addClass("show");
                $("#enviosms").addClass("active");
            } else {
                $("#enviosms-tab").removeClass("active");
                $("#enviosms-tab").addClass("disabled");
                $("#enviosms").removeClass("show");
                $("#enviosms").removeClass("active");
            }
        }

        function estadoTabAsistencia(estado) { 
            if (estado == 'H') {
                $("#asistencia-tab").removeClass("disabled");
                $("#asistencia-tab").addClass("active");
                $("#asistencia").addClass("show");
                $("#asistencia").addClass("active");
            } else {
                $("#asistencia-tab").removeClass("active");
                $("#asistencia-tab").addClass("disabled");
                $("#asistencia").removeClass("show");
                $("#asistencia").removeClass("active");
            }
        }
        //olluen 27/02/2020
        function estadoTabInscripcion(estado) { 
            if (estado == 'H') {
                $("#inscripcion-tab").removeClass("disabled");
                $("#inscripcion-tab").addClass("active");
                $("#inscripcion").addClass("show");
                $("#inscripcion").addClass("active");
            } else {
                $("#inscripcion-tab").removeClass("active");
                $("#inscripcion-tab").addClass("disabled");
                $("#inscripcion").removeClass("show");
                $("#inscripcion").removeClass("active");
            }
        }
        
        function estadoTabVerInscritos(estado) { 
            if (estado == 'H') {
                $("#verInscritos-tab").removeClass("disabled");
                $("#verInscritos-tab").addClass("active");
                $("#verInscritos").addClass("show");
                $("#verInscritos").addClass("active");
            } else {
                $("#verInscritos-tab").removeClass("active");
                $("#verInscritos-tab").addClass("disabled");
                $("#verInscritos").removeClass("show");
                $("#verInscritos").removeClass("active");
            }
        }
        
            

        /* Flujo de tabs del modal. */
        function flujoTabsModal(tabActivo) {
            if (tabActivo == 'listaProgramacion-tab') {     
                //HABILITAR  
                estadoTabListaProgramacion('H');            

                //DESHABILITAR 
                estadoTabRegistroProgramacion('D');                

            }else if (tabActivo == 'registroProgramacion-tab'){
                //HABILITAR             
                estadoTabRegistroProgramacion('H');

                //DESHABILITAR 
                estadoTabListaProgramacion('D');                

            }            
        }

        function estadoTabListaProgramacion(estado) {
            if (estado == 'H') {
                $("#listaProgramacion-tab").removeClass("disabled");
                $("#listaProgramacion-tab").addClass("active");
                $("#listaProgramacion").addClass("show");
                $("#listaProgramacion").addClass("active");
            } else {
                $("#listaProgramacion-tab").removeClass("active");
                $("#listaProgramacion-tab").addClass("disabled");
                $("#listaProgramacion").removeClass("show");
                $("#listaProgramacion").removeClass("active");
            }
        }

        function estadoTabRegistroProgramacion(estado) {
            if (estado == 'H') {
                $("#registroProgramacion-tab").removeClass("disabled");
                $("#registroProgramacion-tab").addClass("active");
                $("#registroProgramacion").addClass("show");
                $("#registroProgramacion").addClass("active");
            } else {
                $("#registroProgramacion-tab").removeClass("active");
                $("#registroProgramacion-tab").addClass("disabled");
                $("#registroProgramacion").removeClass("show");
                $("#registroProgramacion").removeClass("active");
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
                case 'EnviarSMS':                    
                    $loadingGif = $('.loader');
                    break;
                case 'ListaProgramacion':
                    $loadingGif = $('#loading-listaProgramacion');
                    $elemento = $('#udpListaProgramacion');
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
        
         function solonumeros(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            if (key < 48 || key > 57) {
                return false;
            }

            return true;
        }
                
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmActividadEvento" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">EVENTOS</div>                
            </div>    
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="listado" aria-selected="true">Listado</a>
                </li>
                <li class="nav-item">
                    <a href="#actividad" id="actividad-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="actividad" aria-selected="false">Actividades</a>
                </li>
                <li class="nav-item">
                    <a href="#asistencia" id="asistencia-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="asistencia" aria-selected="false">Asistencia</a>
                </li>                 
                <li class="nav-item">
                    <a href="#enviosms" id="enviosms-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="enviosms" aria-selected="false">Enviar SMS</a>
                </li>
                <%--Olluen 27/02/2020--%>
                <li class="nav-item">
                    <a href="#inscripcion" id="inscripcion-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="inscripcion" aria-selected="false">Inscripción</a>
                </li>
                <li class="nav-item">
                    <a href="#verInscritos" id="A1" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="verInscritos" aria-selected="false">Ver Inscritos</a>
                </li>
                <%----------------------%>               
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
                                            <label for="txtDescripcionFiltro" class="col-sm-2 form-control-sm">Evento:</label>
                                            <div class="col-sm-5">                                                                                
                                                <asp:TextBox ID="txtDescripcionFiltro" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                            
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-6">
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
                    <br/>
                    <div class="table-responsive">
                        <div id="loading-lista" class="loading oculto">                            
                            <img src="img/loading.gif">
                        </div>
                        <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_Cco, descripcion_Cco, token_dev"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="descripcion_Cco" HeaderText="EVENTOS" ItemStyle-Width="80%" ItemStyle-Wrap="false"/>
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="20%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnVer" runat="server" 
                                                    CommandName="Ver" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"                                                     
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Ver actividades" 
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea ingresar al formulario de actividades?', 'warning');">
                                                    <span><i class="fa fa-eye"></i></span>
                                                </asp:LinkButton>
                                                 <%--Olluen 27/02/2020--%>
                                                 <asp:LinkButton ID="btnInsc" runat="server" 
                                                    CommandName="Inscripcion" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"                                                     
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Inscripción rápida" 
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea ingresar al formulario de inscripción?', 'warning');">
                                                    <span><i class="fa fa-list-alt"></i></span>
                                                </asp:LinkButton>
                                                 <asp:LinkButton ID="btnVerInsc" runat="server" 
                                                    CommandName="VerInscritos" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"                                                     
                                                    CssClass="btn btn-info btn-sm" 
                                                    ToolTip="Ver inscritos" 
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea ver los iscritos al evento?', 'warning');">
                                                    <span><i class="fa  fa-binoculars"></i></span>
                                                </asp:LinkButton>
                                                <%---------------------%>
                                                 
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
                <div class="tab-pane" id="actividad" role="tabpanel" aria-labelledby="actividad-tab">
                    <asp:UpdatePanel ID="udpActividad" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="panel-cabecera">
                                <div class="card">
                                    <div class="card-header">Lista de Actividades</div>
                                    <div class="card-body">                             
                                        <div class="row">
                                            <label for="txtEvento" class="col-sm-2 col-form-label form-control-sm">Evento:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtEvento" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div> 
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-6">                                                                                                           
                                                <asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-accion btn-danger">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
                                                </asp:LinkButton> 
                                                <asp:LinkButton ID="btnNuevo" runat="server" CssClass="btn btn-accion btn-azul">
                                                    <i class="fa fa-plus-square"></i>
                                                    <span class="text">Nuevo</span>
                                                </asp:LinkButton>                                                  
                                            </div>                                 
                                        </div>                                
                                    </div>                            
                                </div>
                            </div>
                            <br/>
                            <div class="table-responsive"> 
                                <div id="loading-actividad" class="loading oculto">
                                    <img src="img/loading.gif">
                                </div>                                
                                <asp:GridView ID="grwActividad" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_aev" 
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="titulo_Acs" HeaderText="TIPO" ItemStyle-CssClass="uppercase"/>
                                        <asp:BoundField DataField="nombre_aev" HeaderText="ACTIVIDAD" ItemStyle-CssClass="uppercase"/>
                                        <asp:BoundField DataField="cupos_aev" HeaderText="CUPOS"/>
                                        <asp:BoundField DataField="inscritos" HeaderText="INSC."/>
                                        <asp:BoundField DataField="urlEncuesta_aev" HeaderText="ENCUESTA"/>
                                        <asp:BoundField DataField="envioSMS" HeaderText="SMS"/>
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandName="Editar" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar actividad"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnProgramacion" runat="server" 
                                                    CommandName="Programacion" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CssClass="btn btn-info btn-sm" 
                                                    ToolTip="Programación de actividad"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea ingresar al formulario de programación de actividad?', 'warning');">
                                                    <span><i class="fa fa-calendar-alt"></i></span>
                                                </asp:LinkButton>                                                
                                                <asp:LinkButton ID="btnAsistencia" runat="server" 
                                                    CommandName="Asistencia" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Registrar asistencia"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea ingresar al formulario de asistencia?', 'warning');">
                                                    <span><i class="fa fa-user-check"></i></span>
                                                </asp:LinkButton> 
                                                <asp:LinkButton ID="btnSMS" runat="server" 
                                                    CommandName="SMS" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Enviar SMS"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea ingresar al formulario de envío de SMS?', 'warning');">
                                                    <span><i class="fa fa-comment"></i></span>
                                                </asp:LinkButton>                                                                                       
                                            </ItemTemplate>                                        
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br/>
                            </div>
                            <br/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane" id="asistencia" role="tabpanel" aria-labelledby="asistencia-tab">
                    <asp:UpdatePanel ID="udpAsistencia" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="panel-cabecera">
                                <div class="card">
                                    <div class="card-header">Asistencia</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtEventoAsistencia" class="col-sm-2 col-form-label form-control-sm">Evento:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtEventoAsistencia" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtActividadAsistencia" class="col-sm-2 col-form-label form-control-sm">Actividad:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtActividadAsistencia" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtFechaAsistencia" class="col-sm-2 col-form-label form-control-sm">Fecha<span class="requerido">(*)</span>:</label>
                                            <div class="input-group col-sm-3">
                                                <asp:TextBox ID="txtFechaAsistencia" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtDocumentoAsistencia" class="col-sm-2 col-form-label form-control-sm">Nro. Documento<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtDocumentoAsistencia" runat="server" MaxLength="15" CssClass="form-control form-control-sm" 
                                                    onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                            </div>     
                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="btnGuardarAsistencia" runat="server" CssClass="btn btn-accion btn-success" 
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar la asistencia?', 'warning');">
                                                    <i class="fa fa-user-plus"></i>
                                                    <span class="text">Guardar</span>
                                                </asp:LinkButton> 
                                            </div>                                         
                                        </div>
                                        <div class="row">
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                        </div>                                        
                                        <hr/>             
                                        <div class="row">
                                            <div class="col-sm-6">                                                                                                           
                                                <asp:LinkButton ID="btnSalirAsistencia" runat="server" CssClass="btn btn-accion btn-danger">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
                                                </asp:LinkButton> 
                                            </div>                                 
                                        </div>                                          
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>                
                <div class="tab-pane" id="enviosms" role="tabpanel" aria-labelledby="enviosms-tab">
                    <asp:UpdatePanel ID="udpEnvioSMS" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="panel-cabecera">
                                <div class="card">
                                    <div class="card-header">Enviar SMS</div>
                                    <div class="card-body">  
                                        <div class="row">
                                            <label for="txtEventoSMS" class="col-sm-2 col-form-label form-control-sm">Evento:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtEventoSMS" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>     
                                        <div class="row">
                                            <label for="txtActividadSMS" class="col-sm-2 col-form-label form-control-sm">Actividad:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtActividadSMS" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>   
                                        <div class="row">
                                            <label for="txtEncuestaSMS" class="col-sm-2 col-form-label form-control-sm">URL Encuesta<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtEncuestaSMS" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>   
                                        <div class="row">
                                            <label for="txtParticipantesSMS" class="col-sm-2 col-form-label form-control-sm">Participantes:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtParticipantesSMS" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                            <label for="txtTotalSMS" class="col-sm-2 col-form-label form-control-sm">Total Enviar:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtTotalSMS" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div> 
                                        <div class="row">
                                            <label for="txtMensajeSMS" class="col-sm-2 col-form-label form-control-sm">Mensaje<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtMensajeSMS" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="3" AutoComplete="off"/>
                                            </div>
                                        </div>     
                                        <div class="row">
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                        </div>                                     
                                        <hr/>             
                                        <div class="row">
                                            <div class="col-sm-6">                                                                                                           
                                                <asp:LinkButton ID="btnEnviarSMS" runat="server" CssClass="btn btn-accion btn-success" 
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea realizar el envío de SMS?', 'warning');">
                                                    <i class="fa fa-paper-plane"></i>
                                                    <span class="text">Enviar</span>
                                                </asp:LinkButton> 
                                                <asp:LinkButton ID="btnSalirSMS" runat="server" CssClass="btn btn-accion btn-danger">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
                                                </asp:LinkButton> 
                                            </div>                                 
                                        </div>                                
                                    </div>                            
                                </div>
                            </div>
                            <br/>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br/>
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="udpParticipantesSMS" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwParticipantesSMS" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_Pso"
                                    CssClass="display table table-sm" GridLines="None">                                
                                    <Columns>
                                        <asp:BoundField DataField="participante" HeaderText="PARTICIPANTE" ItemStyle-CssClass="uppercase" ItemStyle-Width="80%" ItemStyle-Wrap="false"/>                                    
                                        <asp:BoundField DataField="telefonoCelular_Pso" HeaderText="CELULAR" ItemStyle-CssClass="uppercase" ItemStyle-Width="20%" ItemStyle-Wrap="false"/>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>  
                        <br/>
                    </div>
                    <br/>   
                </div>                
                <div class="tab-pane" id="inscripcion" role="tabpanel" aria-labelledby="inscripcion-tab">
                    <asp:UpdatePanel ID="udpInscripcion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="panel-cabecera">
                                <div class="card">
                                    <div class="card-header">Inscripción Rápida</div>
                                    <div class="card-body">                                                                            
                                        <asp:Label ID="lblmensaje0" runat="server" Text="mensaje" Visible="false"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtCodigo_cco" runat="server" Visible ="false">
                                        </asp:TextBox>
                                        <asp:HiddenField ID="hfCodigo_cco" runat="server" />
                                        <asp:HiddenField ID="hftoken_dev" runat="server" />
                                        
                                        <div class="row">
                                            <label for="lblEventoInsc" class="col-sm-1 col-form-label form-control-sm" style="text-align:left">&nbsp;Evento:</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtEventoInsc" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>     
                                        <div class="row">
                                            <label for="lblTipPart" class="col-sm-1 col-form-label form-control-sm" style="text-align:left" > 
                                            &nbsp;Participante<span class="requerido">(*)</span></label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddTipoPart" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="lblTipDoc" class="col-sm-1 col-form-label form-control-sm" style="text-align:left" > 
                                            &nbsp;Tipo Doc.<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="dpTipoDoc" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                                    <asp:ListItem>DNI</asp:ListItem>
                                                    <asp:ListItem>CARNÉ DE EXTRANJERÍA</asp:ListItem>
                                                    <asp:ListItem Value="PAS">PASAPORTE</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label for="lblDoc" class="col-sm-1 col-form-label form-control-sm" style="text-align:left">Nro. Doc.<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtNroDoc" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:LinkButton ID="lbBuscaPersona" runat="server" CssClass="btn btn-sm btn-success" Text='<i class="fa fa-search"></i>'></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="lblApPat" class="col-sm-1 col-form-label form-control-sm" style="text-align:left">&nbsp;Ap. Paterno<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtApPat" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="lblApMat" class="col-sm-1 col-form-label form-control-sm" style="text-align:left">Ap. Materno:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtApMat" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>     
                                        <div class="row">
                                            <label for="lblNombres" class="col-sm-1 col-form-label form-control-sm" style="text-align:left">&nbsp;Nombres<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="lblCel" class="col-sm-1 col-form-label form-control-sm" style="text-align:left">Nro. Cel.<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtCel" runat="server" onkeypress="javascript:return solonumeros(event)" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                        </div>                                       
                                        
                                        <div class="card-header">Información laboral</div>
                                        <br />
                                        <div class="row">                                                        
                                            <label class="col-sm-2 col-form-label form-control-sm" for="lChkbLabAct" style="text-align:left">&nbsp;Actualmente Ud. No Labora</label>
                                            <div id="div1" class="col-sm-3" runat="server">
                                                <asp:CheckBox ID="chkLabora" runat="server" AutoPostBack="true" />
                                            </div>                                                        
                                        </div>
                                        <div class="row">
                                            <label class="col-sm-1 col-form-label form-control-sm" for="lblEmpresa" style="text-align:left">
                                                &nbsp;Empresa<span class="requerido">(**)</span>:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" name="txtEmpAlum" ID="txtEmpAlum" class="form-control form-control-sm uppercase"></asp:TextBox>
                                            </div>
                                            <label class="col-sm-1 col-form-label form-control-sm" for="lTxtcargo" style="text-align:left">
                                                Cargo<span class="requerido">(**)</span>:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" name="txtCargAlum" ID="txtCargAlum" class="form-control form-control-sm uppercase"></asp:TextBox>
                                            </div>                                                        
                                         </div>
                                        <div class="row">
                                            <label class="col-sm-1 col-form-label form-control-sm" for="lblDireccion" style="text-align:left">
                                                &nbsp;Dirección:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" name="txtDirEmp" ID="txtDirEmp" class="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-sm-1 col-form-label form-control-sm" for="lblTelefono" style="text-align:left">
                                                Teléfono:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" name="txtTelEmp" ID="txtTelEmp" onkeypress="javascript:return solonumeros(event)" class="form-control form-control-sm uppercase"></asp:TextBox>
                                            </div>
                                         </div>
                                        <div class="row">
                                            <label class="col-sm-1 col-form-label form-control-sm" for="lblEmailEmp" style="text-align:left">
                                                &nbsp;Email:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" name="txtEmailEmp" ID="txtEmailEmp" class="form-control form-control-sm uppercase"></asp:TextBox>
                                            </div>                                                                                                    
                                         </div>
                                        <div class="row">
                                            <label class="col-sm-2 col-form-label form-control-sm" for="lblModLab" style="text-align:left">
                                                &nbsp; Ingresé a laborar por<span class="requerido">(**)</span>: </label>
                                            <asp:RadioButtonList ID="rbModLabora" runat="server" 
                                                RepeatDirection="Horizontal" Font-Names="Verdana" Font-Size="12px">
                                                <asp:ListItem Value="1">&nbsp;Oferta Laboral Alumni&nbsp;&nbsp;</asp:ListItem> 
                                                <asp:ListItem Value="0">&nbsp;&nbsp;Oferta Laboral Externa&nbsp;&nbsp;</asp:ListItem>
                                            </asp:RadioButtonList>
                                         </div>
                                         <div class="row">
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(**)</span> Campos obligatorios</label>
                                        </div>                                         
                                    </div>
                                    <div class="card-footer">
                                          <asp:LinkButton ID="lbGuardaInsc" runat="server" CssClass="btn btn-accion btn-verde" 
                                                OnClientClick="return alertConfirm(this, event, '¿Desea registrar la inscripción?', 'warning');">
                                                <i class="fa fa-save"></i>
                                                <span class="text">Guardar</span>
                                           </asp:LinkButton> 
                                           <asp:LinkButton ID="lbSalirInsc" runat="server" CssClass="btn btn-accion btn-danger">
                                                <i class="fa fa-sign-out-alt"></i>
                                                <span class="text">Salir</span>
                                           </asp:LinkButton>    
                                    </div> 
                                </div>
                            </div>
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
                
                <div class="tab-pane" id="verInscritos" role="tabpanel" aria-labelledby="verInscritos-tab">
                    <asp:UpdatePanel ID="udpVerInscritos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="panel-cabecera">
                                <div class="card">
                                    <div class="card-header">Ver Inscritos</div>
                                    <div class="card-body">
                                        <div class="row">                                            
                                            <label for="lblVerInsc" class="col-sm-1 col-form-label form-control-sm" style="text-align:left">&nbsp;Evento:</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtEventoVerInsc" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                                <asp:TextBox ID="txtCcoVerInsc" runat="server" CssClass="form-control form-control-sm uppercase"  Visible ="false" ReadOnly="true" AutoComplete="off"/>  
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="lbSalirVerInsc" runat="server" CssClass="btn btn-accion btn-danger">
                                                <i class="fa fa-sign-out-alt"></i>
                                                <span class="text">Salir</span>
                                                </asp:LinkButton> 
                                            </div>
                                        </div>
                                        <br />
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvListaVerInscritos" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames=""
                                                CssClass="display table table-sm" GridLines="None">
                                                <Columns>
                                                    <asp:BoundField DataField="DNI" HeaderText="NRO. DOC." ItemStyle-Width="20%" ItemStyle-Wrap="false"/>                                                    
                                                    <asp:BoundField DataField="APELLIDOS" HeaderText="APELLIDOS" ItemStyle-Width="30%" ItemStyle-Wrap="false"/>
                                                    <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRES" ItemStyle-Width="30%" ItemStyle-Wrap="false"/>                                                    
                                                    <asp:BoundField DataField="FECHINSC" HeaderText="FECHA INSCRIPCION" ItemStyle-Width="20%" ItemStyle-Wrap="false"/>                                                    
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
            </div>
        </div>

        <!-- Modal Registro de Actividades -->
        <div id="mdlRegistro" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">           
                                <span class="modal-title">REGISTRO DE ACTIVIDAD</span>             
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>                                                
                            </div>
                            <div class="modal-body">
                                <div class="container">
                                    <div class="row">
                                        <label for="txtActividad" class="col-sm-2 form-control-sm">Actividad<span class="requerido">(*)</span>:</label>
                                        <div class="col-sm-10">                                                                                
                                            <asp:TextBox ID="txtActividad" runat="server" MaxLength="100" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                        </div> 
                                    </div> 
                                    <div class="row">
                                        <label for="cmbTipo" class="col-sm-2 form-control-sm">Tipo<span class="requerido">(*)</span>:</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cmbTipo" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                        </div>
                                        <label for="cmbServicioConcepto" class="col-sm-2 form-control-sm">Concepto:</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cmbServicioConcepto" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                        </div>                                        
                                    </div>
                                    <div class="row">
                                        <label for="txtCosto" class="col-sm-2 form-control-sm">Costo (S/):</label>
                                        <div class="col-sm-2">   
                                            <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control form-control-sm" placeholder="0.00"
                                                Text="0.00" Style="text-align: right" onkeypress="javascript:return soloNumerosDecimal(event)" AutoComplete="off"/>
                                        </div>                                        
                                        <label for="txtCupos" class="col-sm-2 form-control-sm">Cupos<span class="requerido">(*)</span>:</label>
                                        <div class="col-sm-2">                                                                                
                                            <asp:TextBox ID="txtCupos" runat="server" CssClass="form-control form-control-sm uppercase" placeholder="0"
                                                Text="0" onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                        </div>                                          
                                        <label for="txtInscritos" class="col-sm-2 form-control-sm">Inscritos:</label>
                                        <div class="col-sm-2">                                                                                
                                            <asp:TextBox ID="txtInscritos" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" 
                                                onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                        </div>   
                                    </div>
                                    <div class="row">
                                        <label for="txtEncuesta" class="col-sm-2 form-control-sm">URL Encuesta:</label>
                                        <div class="col-sm-10">                                                                                
                                            <asp:TextBox ID="txtEncuesta" runat="server" CssClass="form-control form-control-sm" placeholder="http://www.miencuesta.com" AutoComplete="off"/>
                                        </div>   
                                    </div>
                                    <div class="row">
                                        <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                    </div>                                      
                                </div>
                            </div>
                            <div class="modal-footer">                                
                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde" 
                                OnClientClick="return alertConfirm(this, event, '¿Desea registrar la actividad?', 'warning');">
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

        <!-- Modal Registro de Programaciones -->
        <div id="mdlProgramacion" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">PROGRAMACIÓN DE ACTIVIDAD</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a href="#listaProgramacion" id="listaProgramacion-tab" class="nav-link active" data-toggle="tab" role="tab"
                                        aria-controls="listaProgramacion" aria-selected="true">Listado</a>
                                </li>
                                <li class="nav-item">
                                    <a href="#registroProgramacion" id="registroProgramacion-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                                        aria-controls="registroProgramacion" aria-selected="false">Registro</a>
                                </li>
                            </ul>
                            <div class="tab-content" id="contentTabsProgramacion">
                                <div class="tab-pane show active" id="listaProgramacion" role="tabpanel" aria-labelledby="listaProgramacion-tab">
                                    <div class="panel-cabecera">
                                        <asp:UpdatePanel ID="udpFiltrosProgramacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="card">
                                                    <div class="card-header">Lista de Programaciones</div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <asp:LinkButton ID="btnListarProgramacion" runat="server" CssClass="btn btn-accion btn-celeste">
                                                                    <i class="fa fa-sync-alt"></i>
                                                                    <span class="text">Listar</span>
                                                                </asp:LinkButton> 
                                                                <asp:LinkButton ID="btnNuevaProgramacion" runat="server" CssClass="btn btn-accion btn-verde">
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
                                        <div id="loading-listaProgramacion" class="loading oculto">
                                            <img src="img/loading.gif">
                                        </div>
                                        <asp:UpdatePanel ID="udpListaProgramacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:GridView ID="grwListaProgramacion" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_apr"
                                                    CssClass="display table table-sm" GridLines="None">
                                                    <Columns>
                                                        <asp:BoundField DataField="lugar_apr" HeaderText="LUGAR"/>
                                                        <asp:BoundField DataField="fechahoraini_apr" HeaderText="F. INICIO"/>
                                                        <asp:BoundField DataField="fechahorafin_apr" HeaderText="F. FIN"/>
                                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEditarProgramacion" runat="server" 
                                                                    CommandName="Editar" 
                                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                                    CssClass="btn btn-primary btn-sm" 
                                                                    ToolTip="Editar programación"
                                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                                    <span><i class="fa fa-pen"></i></span>
                                                                </asp:LinkButton>                                              
                                                                <asp:LinkButton ID="btnEliminarProgramacion" runat="server" 
                                                                    CommandName="Eliminar" 
                                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                                    CssClass="btn btn-danger btn-sm" 
                                                                    ToolTip="Eliminar programación"
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
                                <div class="tab-pane" id="registroProgramacion" role="tabpanel" aria-labelledby="registroProgramacion-tab">
                                    <div class="panel-cabecera">
                                        <asp:UpdatePanel ID="udpRegistroProgramacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="card">
                                                    <div class="card-header">Registro de Programación</div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <label for="txtLugarProgramacion" class="col-sm-2 col-form-label form-control-sm">Lugar<span class="requerido">(*)</span>:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtLugarProgramacion" runat="server" MaxLength="150" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                                            </div>
                                                        </div>                                                        
                                                        <div class="row">
                                                            <label for="txtFechaProgramacion" class="col-sm-2 col-form-label form-control-sm">Fecha<span class="requerido">(*)</span>:</label>
                                                            <div class="input-group col-sm-3">
                                                                <asp:TextBox ID="txtFechaProgramacion" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                                    onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text">
                                                                        <i class="fa fa-calendar-alt"></i>
                                                                    </span>
                                                                </div>                                                
                                                            </div>                                                            
                                                        </div>
                                                        <div class="row">
                                                            <label for="txtHoraInicio" class="col-sm-2 col-form-label form-control-sm">Hora Inicio<span class="requerido">(*)</span>:</label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox ID="txtHoraInicio" runat="server" MaxLength="5" CssClass="form-control form-control-sm" placeholder="00:00"
                                                                    onkeypress="javascript:return soloNumerosHora(event)" AutoComplete="off"/>
                                                            </div>
                                                            <label for="txtHoraInicio" Style="text-align: left" class="col-sm-3 col-form-label form-control-sm">Formato 24H (HH:MM)</label>
                                                        </div>
                                                        <div class="row">
                                                            <label for="txtHoraFin" class="col-sm-2 col-form-label form-control-sm">Hora Fin<span class="requerido">(*)</span>:</label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox ID="txtHoraFin" runat="server" MaxLength="5" CssClass="form-control form-control-sm" placeholder="00:00"
                                                                    onkeypress="javascript:return soloNumerosHora(event)" AutoComplete="off"/>
                                                            </div>
                                                            <label for="txtHoraFin" Style="text-align: left" class="col-sm-3 col-form-label form-control-sm">Formato 24H (HH:MM)</label>
                                                        </div> 
                                                        <div class="row">
                                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                                        </div>                                                        
                                                    </div>
                                                    <div class="modal-footer">                                
                                                        <asp:LinkButton ID="btnGuardarProgramacion" runat="server" CssClass="btn btn-accion btn-verde" 
                                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar la programación?', 'warning');">
                                                            <i class="fa fa-save"></i>
                                                            <span class="text">Guardar</span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btnSalirProgramacion" runat="server" CssClass="btn btn-accion btn-rojo">
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
        
        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {
            /*Ocultar cargando*/   
            $('.loader').fadeOut("slow");
        });

        /* Flujo de tabs de la página principal. */
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            switch (controlId) {                
                case 'btnListar':
                    AlternarLoading(false, 'Lista');                    
                    break;
                case 'btnEnviarSMS': 
                    AlternarLoading(false, 'EnviarSMS');                    
                    break;
                case 'btnListarProgramacion':
                    AlternarLoading(false, 'ListaProgramacion');                    
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
                case 'btnEnviarSMS': 
                    AlternarLoading(true, 'EnviarSMS');                    
                    break;    
                case 'btnListarProgramacion':
                    AlternarLoading(true, 'ListaProgramacion');                    
                    break;                                               
            }
        });

    </script>
</body>
</html>
