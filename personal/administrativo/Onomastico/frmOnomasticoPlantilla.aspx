<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOnomasticoPlantilla.aspx.vb" Inherits="administrativo_Onomastico_frmOnomasticoPlantilla" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Saludo de Cumpleaños</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../Alumni/css/sweetalert/sweetalert2.min.css">

    <!-- Estilos propios -->
    <link rel="stylesheet" href="../../Alumni/css/estilos.css?1">

    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../../Alumni/js/popper.js"></script>    
    <script src="../../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../../Alumni/js/sweetalert/sweetalert2.js"></script>

    <!-- Scripts propios -->
    <script src="../../Alumni/js/funciones.js?1"></script>

    <script type="text/javascript">
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
                    if (ctl.id == "btnGuardar"){
                        AlternarLoading(false, 'Registro');                        
                    }

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

        /* Abrir y cerrar el modales. */
        function openModal(elemento) {
            switch (elemento) {
                case 'Header':
                    $('#mdlHeader').modal('show');                   
                    break;

                case 'Footer':
                    $('#mdlFooter').modal('show');                   
                    break;

                case 'Tarjeta':
                    $('#mdlTarjeta').modal('show');                   
                    break;
            }
        }

        function closeModal(elemento) {
            switch (elemento) {
                case 'Header':
                    $('#mdlHeader').modal('hide');                   
                    break;

                case 'Footer':
                    $('#mdlFooter').modal('hide');                   
                    break;  
                    
                case 'Tarjeta':
                    $('#mdlTarjeta').modal('hide');                   
                    break;
            }
        }
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmOnomasticoPlantilla" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">SALUDO DE CUMPLEAÑOS</div>
            </div>    
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#plantilla" id="plantilla-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="plantilla" aria-selected="true">Plantilla</a>
                </li>                
            </ul>    
            <div class="tab-content" id="contentTabs">
                <div class="tab-pane show active" id="plantilla" role="tabpanel" aria-labelledby="plantilla-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpPlantilla" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">Cabecera</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtHeader" class="col-sm-2 col-form-label form-control-sm">Imagen:</label>
                                            <div class="col-sm-7">
                                                <div class="row">
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtHeader" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btnVerHeader" runat="server" CssClass="btn btn-accion btn-azul">
                                                            <i class="fa fa-image"></i>
                                                            <span class="text">Ver</span>
                                                        </asp:LinkButton>                                                          
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtCargarHeader" class="col-sm-2 col-form-label form-control-sm">Cargar Imagen:</label>
                                            <div class="col-sm-7">
                                                <asp:FileUpload ID="txtCargarHeader" runat="server" CssClass="form-control form-control-sm"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Dimensiones Sugeridas: Ancho = 600px ; Alto: 350px</label>
                                        </div>                                        
                                    </div>
                                </div>
                                <br/>
                                <div class="card">
                                    <div class="card-header">Pie de Página</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtFooter" class="col-sm-2 col-form-label form-control-sm">Imagen:</label>
                                            <div class="col-sm-7">
                                                <div class="row">
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtFooter" runat="server" CssClass="form-control form-control-sm" ReadOnly="true" AutoComplete="off"/>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btnVerFooter" runat="server" CssClass="btn btn-accion btn-azul">
                                                            <i class="fa fa-image"></i>
                                                            <span class="text">Ver</span>
                                                        </asp:LinkButton>                                                          
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtCargarFooter" class="col-sm-2 col-form-label form-control-sm">Cargar Imagen:</label>
                                            <div class="col-sm-7">
                                                <asp:FileUpload ID="txtCargarFooter" runat="server" CssClass="form-control form-control-sm"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-sm-12 label-requerido"><span class="requerido">(*)</span> Dimensiones Sugeridas: Ancho = 600px ; Alto: 350px</label>
                                        </div>
                                    </div>                                    
                                </div>
                                <br/>
                                <hr/>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center;">
                                        <asp:LinkButton ID="btnVisualizar" runat="server" CssClass="btn btn-accion btn-naranja">
                                            <i class="fa fa-image"></i>
                                            <span class="text">Visualizar</span>
                                        </asp:LinkButton>                                         
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar la plantilla de cumpleaños?', 'warning');"
                                            OnClick="btnGuardar_Click">
                                            <i class="fa fa-save"></i>
                                            <span class="text">Guardar</span>
                                        </asp:LinkButton>                                                                                                                                                                  
                                    </div>                                 
                                </div>                                
                            </ContentTemplate>

                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnGuardar" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>    
        </div>

        <!-- Modal Visualizar Header -->
        <div id="mdlHeader" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpHeader" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">           
                                <span class="modal-title">CABECERA</span>             
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>                                                
                            </div> 
                            <div class="modal-body">
                                <div class="panel-cabecera">
                                    <div class="card" style="align-items: center;">
                                        <asp:Image ID="imgHeader" runat="server" ImageUrl="" Width="600px" Height="350px" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">                                                                
                                <button type="button" id="btnCerrarHeader" class="btn btn-danger" data-dismiss="modal">
                                    <i class="fa fa-sign-out-alt"></i>
                                    <span class="text">Salir</span>
                                </button>                                                                                         
                            </div>                                                       
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>        

        <!-- Modal Visualizar Footer -->
        <div id="mdlFooter" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpFooter" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">           
                                <span class="modal-title">PIE DE PÁGINA</span>             
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>                                                
                            </div> 
                            <div class="modal-body">
                                <div class="panel-cabecera">
                                    <div class="card" style="align-items: center;">
                                        <asp:Image ID="imgFooter" runat="server" ImageUrl="" Width="600px" Height="350px" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">                                                                
                                <button type="button" id="btnCerrarFooter" class="btn btn-danger" data-dismiss="modal">
                                    <i class="fa fa-sign-out-alt"></i>
                                    <span class="text">Salir</span>
                                </button>                                                                                         
                            </div>                                                       
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        
        <!-- Modal Visualizar Tarjeta -->
        <div id="mdlTarjeta" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpTarjeta" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">           
                                <span class="modal-title">TARJETA DE CUMPLEAÑOS</span>             
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>                                                
                            </div> 
                            <div class="modal-body">
                                <div class="panel-cabecera">
                                    <div class="card" style="align-items: center;">
                                        <table border=0 style="text-align: center;">
                                            <tr>
                                                <td><asp:Image ID="imgTarjetaHeader" runat="server" ImageUrl="" Width="540px" Height="315px" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-size:20pt; font-family: 'Trebuchet MS' ,sans-serif; color:dimgray">Apellidos y Nombres del colaborador</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><asp:Image ID="imgTarjetaFooter" runat="server" ImageUrl="" Width="540px" Height="315px" /></td>
                                            </tr>                                            
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">                                                                
                                <button type="button" id="btnCerrarTarjeta" class="btn btn-danger" data-dismiss="modal">
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
