<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCategoria.aspx.vb" Inherits="Alumni_frmCategoria" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />    
    <title>Categorías</title>
    <!-- Estilos externos -->         
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="css/datatables/jquery.dataTables.min.css">    

    <!-- Estilos propios -->            
    <link rel="stylesheet" href="css/estilos.css?1">
</head>
<body>
    <form id="frmCategoria" runat="server">  
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
            Text="Su solicitud está siendo procesada..." Title="Por favor espere"/>        
        <div class="container-fluid">    
            <div class="panel-cabecera">
                <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>                              
                        <div class="card">                        
                            <div class="row title">CATEGORÍAS DEL MÓDULO ALUMNI</div>
                        </div>
                        <div class="card">
                            <div class="card-header">Filtros de Búsqueda</div>
                            <div class="card-body">
                                <div class="row">
                                    <label for="cmbGrupoFiltro" class="col-sm-1 col-form-label form-control-sm">Grupo:</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="cmbGrupoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true"></asp:DropDownList>                                    
                                    </div>
                                    <label for="txtNombreFiltro" class="col-sm-1 col-form-label form-control-sm">Nombre:</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtNombreFiltro" runat="server" CssClass="form-control form-control-sm uppercase" placeholder="Nombre" />
                                    </div>                                
                                </div>
                                <hr/>
                                <div class="row">
                                    <div class="col-sm-6">                                    
                                        <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-accion btn-celeste">
                                            <i class="fa fa-sync-alt"></i>
                                            <span class="text">Listar</span>
                                        </asp:LinkButton>                                    
                                        <asp:LinkButton ID="btnRegistrarCategoria" runat="server" CssClass="btn btn-accion btn-azul">
                                            <i class="fa fa-plus-square"></i>
                                            <span class="text">Nueva Categoría</span>
                                        </asp:LinkButton>                                    
                                        <asp:LinkButton ID="btnRegistrarGrupo" runat="server" CssClass="btn btn-accion btn-verde">
                                                <i class="fa fa-plus-square"></i>
                                                <span class="text">Nuevo Grupo</span>
                                        </asp:LinkButton>                                  
                                    </div>
                                </div> 
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>  
            </div>                                 
            <br/>
            <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_cat, nombre_cat, grupo_cat"
                        CssClass="display table table-sm" GridLines="None">
                        <Columns>                            
                            <asp:BoundField DataField="nombre_cat" HeaderText="NOMBRE"/>                             
                            <asp:BoundField DataField="grupo_cat" HeaderText="GRUPO"/>                           
                            <asp:BoundField DataField="fecha_mod" HeaderText="ÚLT. MOD."/>         
                            <asp:TemplateField HeaderText="OPERACIONES">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                        CssClass="btn btn-primary btn-sm" OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                        <span><i class="fa fa-pen"></i></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                        CssClass="btn btn-danger btn-sm" OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el registro seleccionado?', 'error');">
                                        <span><i class="fa fa-trash"></i></span>
                                    </asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>                                                               
                        </Columns>                                        
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>                      
        </div>

        <!-- Modal Registro de Categorías -->
        <div id="mdlRegistro" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpContenidoModal" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">           
                                <span class="modal-title">REGISTRAR CATEGORÍA</span>             
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>                                                
                            </div>
                            <div class="modal-body">
                                <div class="container">
                                    <div class="row">    
                                        <label class="col-sm-3 form-control-sm">Grupo:</label>
                                        <div class="col-sm-6">                                                                                
                                            <asp:TextBox ID="txtGrupo" runat="server" CssClass="form-control form-control-sm uppercase" placeholder="Grupo" />
                                        </div>                                                               
                                    </div>                                    
                                    <div class="row">
                                        <label class="col-sm-3 form-control-sm">Nombre:</label>
                                        <div class="col-sm-6">                                                                                
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm uppercase" placeholder="Nombre" />
                                        </div>                          
                                    </div>                            
                                </div>                       
                            </div>
                            <div class="modal-footer">                                
                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde" OnClientClick="return validarRegistro();">
                                    <i class="fa fa-save"></i>
                                    <span class="text">Guardar</span>
                                </asp:LinkButton> 
                                <button type="button" id="btnSalir" class="btn btn-danger" data-dismiss="modal">
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

    <!-- Scripts externos -->    
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
    <script src="../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../assets/iframeresizer/iframeResizer.min.js"></script>
    <script src="../assets/fileDownload/jquery.fileDownload.js"></script>    
    <script src="js/popper.js"></script>    
    <script src="js/alerts/sweetalert.min.js"></script>
    <script src="js/datatables/jquery.dataTables.min.js?1"></script>        

    <!-- Scripts propios -->    
    <script src="js/funciones.js"></script>    

    <script type="text/javascript">

        $(document).ready(function() {
            $('#cmbGrupoFiltro').selectpicker({
                size: 6,
            });
        });

        function formatoGrilla(){
            // Setup - add a text input to each footer cell
            $('#grwLista thead tr').clone(true).appendTo( '#grwLista thead' );
            $('#grwLista thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Search '+ title+'" />' );
        
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
                orderCellsTop: true,
                fixedHeader: true
            } );
        }

        /* FUNCIONES */
        function validarRegistro(){
            if ($('#<%=txtNombre.ClientID%>').val().trim() == '') {
                alert("Ingrese un Nombre");
                $('#<%=txtNombre.ClientID%>').focus();
                return false;
            }
                            
            if ($('#<%=txtGrupo.ClientID%>').val().trim() == '') {
                alert("Ingrese un Grupo");
                $('#<%=txtGrupo.ClientID%>').focus();
                return false;
            }  

            closeModal();

            return true;
        }

        function showMessage(message, messagetype) {                             
            swal({
                title: message,                
                icon: messagetype
            })
        }    

        function alertConfirm(ctl, event, titulo, icono) {
            // STORE HREF ATTRIBUTE OF LINK CTL (THIS) BUTTON
            var defaultAction = $(ctl).prop("href");

            // CANCEL DEFAULT LINK BEHAVIOUR
            event.preventDefault();            

            swal({
                title: titulo,                
                icon: icono,
                buttons: true,
                dangerMode: true,
            })
            .then((isConfirm) => {
                if (isConfirm) {
                    window.location.href = defaultAction;
                    return true;
                } else {
                    return false;
                }
            });
        }

        function openModal(accion) {            
            $('#<%=txtGrupo.ClientID%>').prop('readonly', true);            
            if (accion == 'Nuevo') {                                    
                $('#<%=txtNombre.ClientID%>').val('');                                                                            
            } else if (accion == 'NuevoGrupo') {                
                $('#<%=txtGrupo.ClientID%>').prop('readonly', false);
                $('#<%=txtGrupo.ClientID%>').val('');
                $('#<%=txtNombre.ClientID%>').val('');
            }            
            $('#mdlRegistro').modal('show');            
        }

        function closeModal() {
            $('#mdlRegistro').modal('hide');
        }

    </script>
</body>
</html>
