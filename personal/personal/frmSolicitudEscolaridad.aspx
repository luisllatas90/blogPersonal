<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitudEscolaridad.aspx.vb" Inherits="personal_frmSolicitudEscolaridad" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />  
    <title>Solicitud de Escolaridad</title>

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

        function udpListaUpdate() {
            formatoGrilla();
        }

        /* Dar formato a la grilla. */
        function formatoGrilla(){        
            var table = $('#grwLista').DataTable( {
                orderCellsTop: true
            } );            
        }

        function udpListaSolicitudUpdate() {
            formatoGrillaSolicitud();
        }

        /* Dar formato a la grilla. */
        function formatoGrillaSolicitud(){        
            var tableSol = $('#grwListaSolicitud').DataTable( {
                orderCellsTop: true
            } );             
        }

        function udpAgregarDerechoHabienteUpdate(){
            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });             
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
            $('#mdl'+elemento).modal('show');                                       
        }

        function closeModal(elemento) {   
            $('#mdl'+elemento).modal('hide');        
        }        
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmSolicitudEscolaridad" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">SOLICITUD DE ESCOLARIDAD</div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <p style="margin-bottom: 0px;"><b>Instrucciones:</b></p>
                            <p style="font-size: 14px; margin-bottom: 0px;">- Agregar el(los) derecho habiente(s) a la solicitud de escolaridad haciendo clic en el botón "Agregar" que se encuentra en cada ítem de la lista de derecho habientes.</p>
                            <p style="font-size: 14px; margin-bottom: 0px;">- Completar los datos para realizar el registro de cada derecho habiente en la solicitud.</p>
                            <p style="font-size: 14px; margin-bottom: 0px;">- En caso quiera retirar algún derecho habiente de la solicitud, deberá hacer clic en el botón "Quitar" que se encuentra en cada ítem de la lista de solicitud.</p>
                            <p style="font-size: 14px; margin-bottom: 0px;">- Hacer clic en el botón "Solicitar Escolaridad".</p>
                            <p style="font-size: 14px; margin-bottom: 0px;">- Finalmente descargar el formato de solicitud haciendo clic en el botón "Descargar PDF".</p>                        
                        </div>
                    </div>
                </div>
            </div>
            <br/>
            <div class="card">
                <div class="card-header">Derecho Habientes</div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_dhab, derechohabiente, Edad, estado_soe, IdArchivosCompartidosRecibo, IdArchivosCompartidosDNI"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="derechohabiente" HeaderText="APELLIDOS Y NOMBRES" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="fechaNacimiento_dhab" HeaderText="F.NACIMIENTO" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Edad" HeaderText="EDAD" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="estado" HeaderText="ESTADO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />                                        
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
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
                                                <asp:LinkButton ID="btnAgregar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Agregar" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Agregar a solicitud">
                                                    <span><i class="fas fa-user-plus"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                    </Columns>                                    
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>                    
                </div>
            </div>
            <br/>
            <div class="card">
                <div class="card-header">Derecho Habientes a Generar Solicitud</div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="udpListaSolicitud" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwListaSolicitud" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_dhab, codigo_niv, codigo_gra, centro_estudios, IdArchivosCompartidosRecibo, IdArchivosCompartidosDNI"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="derechohabiente" HeaderText="APELLIDOS Y NOMBRES" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Edad" HeaderText="EDAD" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="nivel" HeaderText="NIVEL" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="grado" HeaderText="GRADO" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="centro_estudios" HeaderText="CENTRO ESTUDIOS" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
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
                                                <asp:LinkButton ID="btnQuitar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Quitar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Quitar de solicitud"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea quitar el derecho habiente de la solicitud de escolaridad?', 'warning');">
                                                    <span><i class="fas fa-user-minus"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                    </Columns>                                        
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>                 
                </div>
                <div class="card-footer" style="text-align: center;">
                    <asp:UpdatePanel ID="udpBotones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:LinkButton ID="btnGenerar" runat="server" CssClass="btn btn-accion btn-azul"                                                    
                                OnClientClick="return alertConfirm(this, event, '¿Desea registrar la solicitud de escolaridad?', 'warning');">
                                <i class="fa fa-save"></i>
                                <span class="text">Solicitar Escolaridad</span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDescargarPDF" runat="server" CssClass="btn btn-accion btn-verde">
                                <i class="fa fa-sign-out-alt"></i>
                                <span class="text">Descargar PDF</span>
                            </asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <br/><br/>
            </div>
        </div>

        <!-- Modal Agregar Derecho Habiente a Solicitud -->
        <div id="mdlAgregarDerechoHabiente" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">AGREGAR DERECHO HABIENTE A SOLICITUD</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpAgregarDerechoHabiente" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">Derecho Habiente</div>
                                    <div class="card-body">
                                        <input type="hidden" name="txtCodigoDhab" id="txtCodigoDhab" runat="server" />
                                        <div class="row">
                                            <label for="txtNombre" class="col-sm-2 form-control-sm">Nombre:</label>
                                            <div class="col-sm-6">                                                                                
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off" Readonly="true"/>
                                            </div> 
                                            <label for="txtEdad" class="col-sm-1 form-control-sm">Edad:</label>
                                            <div class="col-sm-1">                                                                                
                                                <asp:TextBox ID="txtEdad" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off" Readonly="true"/>
                                            </div>                                          
                                        </div>
                                        <div class="row">
                                            <label for="cmbNivelEscolaridad" class="col-sm-2 col-form-label form-control-sm">Nivel:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbNivelEscolaridad" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>      
                                            <label for="cmbGrado" class="col-sm-1 col-form-label form-control-sm">Grado:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbGrado" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>                                    
                                            </div>                           
                                        </div>
                                        <div class="row" style="padding-top: 5px;">
                                            <label for="txtCentroEstudios" class="col-sm-2 form-control-sm">Centro Estudios:</label>
                                            <div class="col-sm-8">                                                                                
                                                <asp:TextBox ID="txtCentroEstudios" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                                                                     
                                        </div>
                                        <div class="row"> 
                                            <label for="txtArchivoRecibo" class="col-sm-2 col-form-label form-control-sm">Recibo Matrícula:</label> 
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="txtArchivoRecibo" runat="server" CssClass="form-control form-control-sm" />
                                            </div>
                                        </div>
                                        <div class="row"> 
                                            <label for="txtArchivoDNI" class="col-sm-2 col-form-label form-control-sm">Copia DNI:</label> 
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="txtArchivoDNI" runat="server" CssClass="form-control form-control-sm" />
                                            </div>
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: center;">
                                                <asp:LinkButton ID="btnAgregarDerechoHabiente" runat="server" CssClass="btn btn-accion btn-verde"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea agregar el derecho habiente a la solicitud?', 'warning');"
                                                    OnClick="btnAgregarDerechoHabiente_Click">
                                                    <i class="fa fa-plus-square"></i>
                                                    <span class="text">Agregar</span>
                                                </asp:LinkButton>                                                                                                                                                                 
                                            </div>  
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>

                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnAgregarDerechoHabiente" />
                            </Triggers>
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
            /*Ocultar cargando*/   
            $('.loader').fadeOut("slow");
        });        
    </script>
</body>
</html>
