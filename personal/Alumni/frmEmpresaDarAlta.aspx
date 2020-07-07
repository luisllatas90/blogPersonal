<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmpresaDarAlta.aspx.vb" Inherits="Alumni_frmEmpresaDarAlta" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Dal Alta Empresa</title>
    <!-- Estilos externos -->         
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="css/datatables/jquery.dataTables.min.css">   
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

    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    
    <script type="text/javascript">
        function udpRegistroUpdate() {
            $('#cmbTipoEmpresa').selectpicker({
                size: 6,
            });

            $('#cmbSector').selectpicker({
                size: 6,
            });

            $('#cmbDepartamento').selectpicker({
                size: 6,
            });

            $('#cmbProvincia').selectpicker({
                size: 6,
            });

            $('#cmbDistrito').selectpicker({
                size: 6,
            });

            $('#cmbEstado').selectpicker({
                size: 6,
            });

            $('#cmbTelefono').selectpicker({
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
                    $loadingGif = $('#loading-lista');
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
    
    <form id="frmEmpresaAprobar" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>        
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>
        
        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">ALTA DE EMPRESAS</div>
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
                                            <label for="txtNombreFiltro" class="col-sm-2 form-control-sm">Nombre / RUC:</label>
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
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_emp, ruc_emp"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="nombreComercial_emp" HeaderText="NOMBRE COMERCIAL" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="ruc_emp" HeaderText="RUC" />
                                        <asp:BoundField DataField="telefono_completo" HeaderText="TELÉFONO" />
                                        <asp:BoundField DataField="celular_emp" HeaderText="CELULAR" />
                                        <asp:BoundField DataField="correo_emp" HeaderText="CORREO" />
                                        <asp:BoundField DataField="estado_cat" HeaderText="ESTADO" />
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDarAlta" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="DarAlta" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Dar alta a empresa"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea dar alta a la empresa seleccionada?', 'warning');">
                                                    <span><i class="fa fa-check-circle"></i></span>
                                                </asp:LinkButton>       
                                                <asp:LinkButton ID="btnRechazar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Rechazar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Rechazar empresa"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea rechazar la empresa seleccionada?', 'warning');">
                                                    <span><i class="fa fa-times-circle"></i></span>
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
                                    <div class="card-header">Información de la Empresa</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtRuc" class="col-sm-2 col-form-label form-control-sm">RUC<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtRuc" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtNombreComercial" class="col-sm-2 col-form-label form-control-sm">Nombre Comercial:</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtNombreComercial" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtRazonSocial" class="col-sm-2 col-form-label form-control-sm">Razón Social:</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtAbreviatura" class="col-sm-2 col-form-label form-control-sm">Abreviatura:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtAbreviatura" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>                                        
                                        <div class="row">
                                            <label for="cmbTipoEmpresa" class="col-sm-2 col-form-label form-control-sm">Tipo:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbTipoEmpresa" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" Enabled="False" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbSector" class="col-sm-1 col-form-label form-control-sm">Sector:</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="cmbSector" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" Enabled="False" AutoComplete="off"/>                                    
                                            </div> 
                                        </div>
                                        <div class="row">
                                            <label for="cmbDepartamento" class="col-sm-2 col-form-label form-control-sm">Departamento:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbDepartamento" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" Enabled="False" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbProvincia" class="col-sm-1 col-form-label form-control-sm">Provincia:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbProvincia" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" Enabled="False" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbDistrito" class="col-sm-1 col-form-label form-control-sm">Distrito:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbDistrito" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" Enabled="False" AutoComplete="off"/>                                    
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtDireccion" class="col-sm-2 col-form-label form-control-sm">Dirección:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                            <label for="txtDireccionWeb" class="col-sm-1 col-form-label form-control-sm">Web:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtDireccionWeb" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                            </div>              
                                        </div>
                                        <div class="row">
                                            <label for="txtCorreo" class="col-sm-2 col-form-label form-control-sm">Correo:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                            </div> 
                                            <label for="cmbTelefono" class="col-sm-1 col-form-label form-control-sm">Teléfono:</label>
                                            <div class="col-sm-4">
                                                <div class="row">                                                    
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="cmbTelefono" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" data-live-search="true" Enabled="False" AutoComplete="off"/>                                    
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtCelular" class="col-sm-2 col-form-label form-control-sm">Celular:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                            </div> 
                                            <label for="chkAccesoCampus" class="col-sm-2 col-form-label form-control-sm">Acceso Campus:</label>
                                            <div class="col-sm-1">                                                
                                                <asp:CheckBox ID="chkAccesoCampus" AutoPostBack="True" OnClick="return false;" runat="server"/>
                                            </div>                                             
                                        </div>
                                        <div class="row">
                                            <label for="txtComentario" class="col-sm-2 col-form-label form-control-sm">Comentario<span class="requerido">(*)</span>:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtComentario" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" TextMode="MultiLine" Rows="3" AutoComplete="off"/>
                                            </div> 
                                        </div>
                                        <div class="row">
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Campos obligatorios</label>
                                        </div>                                          
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: center;">
                                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Realmente desea culminar el registro?', 'warning');">
                                                    <i class="fa fa-save"></i>
                                                    <span class="text">Guardar</span>
                                                </asp:LinkButton>                                                                     
                                                <asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-accion btn-rojo">
                                                    <i class="fa fa-sign-out-alt"></i>
                                                    <span class="text">Salir</span>
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
            udpRegistroUpdate();
            __doPostBack('<%= btnListar.UniqueID %>', '');

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
                    break;
                case 'btnGuardar':
                    AlternarLoading(false, 'Registro');                    
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
            }
        });
    </script>
</body>
</html>

