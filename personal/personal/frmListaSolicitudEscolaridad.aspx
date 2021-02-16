<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaSolicitudEscolaridad.aspx.vb" Inherits="personal_frmListaSolicitudEscolaridad" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />  
    <title>Lista de Solicitudes de Escolaridad</title>

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
                    $elemento = $('#udpLista');               
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

    <form id="frmListaSolicitudEscolaridad" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">LISTA DE SOLICITUDES DE ESCOLARIDAD</div>
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
                                    <div class="card-header">Filtros de Búsqueda</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="cmbAnioFiltro" class="col-sm-2 col-form-label form-control-sm">Año:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbAnioFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-accion btn-celeste">
                                                    <i class="fa fa-sync-alt"></i>
                                                    <span class="text">Listar</span>
                                                </asp:LinkButton>  
                                                <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-accion btn-verde">
                                                    <i class="fa fa-file-excel"></i>
                                                    <span class="text">Exportar</span>
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
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_soe, codigo_dhab, codigo_per, IdArchivosCompartidosRecibo, IdArchivosCompartidosDNI"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="codigo_per" HeaderText="CÓDIGO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="dni_per" HeaderText="DNI TRAB." ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="trabajador" HeaderText="TRABAJADOR" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="dni_dhab" HeaderText="DNI DER.HAB." ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="derecho_habiente" HeaderText="DERECHO HABIENTE" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="fechaNac_dhab" HeaderText="F.NACIMIENTO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="centro_estudios" HeaderText="CENTRO DE ESTUDIOS" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="nivel" HeaderText="NIVEL" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="grado" HeaderText="GRADO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="tipo_personal" HeaderText="TIPO TRAB." ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="dedicacion" HeaderText="DEDICACIÓN" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="inicio_labores" HeaderText="INICIO LABORES" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="inicio_contrato" HeaderText="INICIO CONTRATO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="fin_contrato" HeaderText="FIN CONTRATO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="tipo_contrato" HeaderText="TIPO CONTRATO" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="documentos_adjuntos" HeaderText="DOCUMENTOS ADJUNTOS" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="DOCUMENTOS" ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnVerRecibo" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Recibo" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Ver Recibo">
                                                    <span><i class="fas fa-file"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnVerDNI" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="DNI" 
                                                    CssClass="btn btn-info btn-sm" 
                                                    ToolTip="Ver DNI">
                                                    <span><i class="far fa-id-card"></i></span>
                                                </asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br/>
                </div>
                <br/>
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
                            
            }
        });    
    </script>
</body>
</html>
