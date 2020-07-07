<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSincronizarDiagramasER.aspx.vb" Inherits="Alumni_frmSincronizarDiagramasER" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Sincronizar Diagramas ER</title>

    <!-- Estilos externos -->         
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="css/datatables/jquery.dataTables.min.css">   
    <link rel="stylesheet" href="css/sweetalert/sweetalert2.min.css">

    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/estilos.css">

    <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>        
    <script src="js/popper.js"></script>    
    <script src="js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="js/sweetalert/sweetalert2.js"></script>
    <script src="js/datatables/jquery.dataTables.min.js?1"></script> 
    
    <!-- Scripts propios -->
    <script src="js/funciones.js?2"></script>   

    <script type="text/javascript">

        /* Scripts a ejecutar al actualizar los paneles.*/
        function udpFiltrosUpdate() {
            $('#cmbSRVOrigen').selectpicker({
                size: 6,
            });

            $('#cmbBDOrigen').selectpicker({
                size: 6,
            });

            $('#cmbSRVDestino').selectpicker({
                size: 6,
            });
            
            $('#cmbBDDestino').selectpicker({
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

        function AlternarLoading(retorno, elemento) {
            var $loadingGif ;
            var $elemento ;

            switch (elemento) { 
                case 'Lista':                      
                    $loadingGif = $('.loader');                  
                    break;     
                case 'Sincronizacion':                    
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

    <form id="frmSincronizarDiagramaER" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">SINCRONIZAR DIAGRAMAS ENTIDAD-RELACIÓN</div>
            </div> 
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="listado" aria-selected="true">Listado</a>
                </li>
            </ul>
            <div class="tab-content" id="contentTabs">
                <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">Bases de Datos</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="cmbSRVOrigen" class="col-sm-2 col-form-label form-control-sm">SRV Origen:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbSRVOrigen" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbSRVDestino" class="col-sm-2 col-form-label form-control-sm">SRV Destino:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbSRVDestino" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>                                           
                                        </div>
                                        <div class="row">                                            
                                            <label for="cmbBDOrigen" class="col-sm-2 col-form-label form-control-sm">BD Origen:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbBDOrigen" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div> 
                                            <label for="cmbBDDestino" class="col-sm-2 col-form-label form-control-sm">BD Destino:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbBDDestino" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-accion btn-celeste">
                                                    <i class="fa fa-sync-alt"></i>
                                                    <span class="text">Listar</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnSincronizar" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea realizar la sincronizacion de todos los diagramas?', 'warning');">
                                                    <i class="fa fa-random"></i>
                                                    <span class="text">Sincronizar</span>
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
                        <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" 
                                    DataKeyNames="diagram_id_origen, diagram_name_origen, diagram_id_destino, diagram_name_destino, sincronizar, server_name_origen, server_name_destino, database_name_origen, database_name_destino"
                                    CssClass="display table table-sm" GridLines="None" OnRowDataBound="grwLista_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="server_name_origen" HeaderText="SERVIDOR ORIGEN" ItemStyle-Width="15%" ItemStyle-Wrap="false"/>
                                        <asp:BoundField DataField="diagram_name_origen" HeaderText="DIAGRAMA ORIGEN" ItemStyle-Width="30%" ItemStyle-Wrap="false"/>
                                        <asp:BoundField DataField="server_name_destino" HeaderText="SERVIDOR DESTINO" ItemStyle-Width="15%" ItemStyle-Wrap="false"/>
                                        <asp:BoundField DataField="diagram_name_destino" HeaderText="DIAGRAMA DESTINO" ItemStyle-Width="30%" ItemStyle-Wrap="false"/>                                        
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSincronizarDiagrama" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="SincronizarDiagrama" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Sincronizar diagrama"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea sincronizar el diagrama seleccionado?', 'warning');">
                                                    <span><i class="fa fa-random"></i></span>
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
    </form>

    <script type="text/javascript">
        var controlId = '';

        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {            
            udpFiltrosUpdate();                 

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
                case 'btnSincronizar':   
                    AlternarLoading(false, 'Sincronizacion');
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
                case 'btnSincronizar':   
                    AlternarLoading(true, 'Sincronizacion');
                    break;                
            }
        });
    </script>
</body>
</html>
