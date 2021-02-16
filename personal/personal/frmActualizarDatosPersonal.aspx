<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActualizarDatosPersonal.aspx.vb" Inherits="personal_frmActualizarDatosPersonal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Actualizar Datos de Personal</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">                    
    <link rel="stylesheet" href="../Alumni/css/sweetalert/sweetalert2.min.css">     

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../Alumni/css/estilos.css?1">

    <style type="text/css">
        .row {            
            padding-top: 10px !important;    
        }

        .btn.dropdown-toggle { 
            display: flex !important; 
            align-items: center !important;
        }

        .filter-option { 
            position: relative !important;
        }
    </style>

    <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../Alumni/js/popper.js"></script>    
    <script src="../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>
    
    <!-- Scripts propios -->
    <script src="../Alumni/js/funciones.js?1"></script>

    <script type="text/javascript">
        function udpRegistroUpdate() { 
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
    </script>
</head>
<body>
    <form id="frmActualizarDatosPersonal" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <asp:UpdatePanel ID="udpRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <label for="txtEmailPrincipal" class="col-sm-2 col-form-label form-control-sm">Email 1:</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtEmailPrincipal" runat="server" MaxLength="50" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                </div> 
                                <label for="txtEmailAlternativo" class="col-sm-1 col-form-label form-control-sm">Email 2:</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtEmailAlternativo" runat="server" MaxLength="400" CssClass="form-control form-control-sm" AutoComplete="off"/>
                                </div> 
                            </div>
                            <div class="row">
                                <label for="txtTelefono" class="col-sm-2 col-form-label form-control-sm">Teléfono:</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtTelefono" runat="server" MaxLength="100" CssClass="form-control form-control-sm" onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                </div>                                
                                <label for="cmbOperadorInternet" class="col-sm-2 col-form-label form-control-sm">Operador Internet:</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cmbOperadorInternet" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>
                                </div>   
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtOperadorInternet" runat="server" MaxLength="200" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                </div>                              
                            </div>                              
                            <div class="row">
                                <label for="txtCelular" class="col-sm-2 col-form-label form-control-sm">Móvil:</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtCelular" runat="server" MaxLength="100" CssClass="form-control form-control-sm" onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                </div>                                 
                                <label for="cmbOperadorMovil" class="col-sm-2 col-form-label form-control-sm">Operador Móvil:</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cmbOperadorMovil" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>
                                </div>   
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtOperadorMovil" runat="server" MaxLength="200" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                </div>                              
                            </div>   
                            <div class="row">
                                <label for="cmbDepartamento" class="col-sm-2 col-form-label form-control-sm">Departamento:</label>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="cmbDepartamento" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>                                    
                                </div>
                                <label for="cmbProvincia" class="col-sm-1 col-form-label form-control-sm">Provincia:</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cmbProvincia" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>                                    
                                </div>
                                <label for="cmbDistrito" class="col-sm-1 col-form-label form-control-sm">Distrito:</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cmbDistrito" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>                                    
                                </div>
                            </div>                                                  
                            <div class="row">
                                <label for="txtDireccion" class="col-sm-2 col-form-label form-control-sm">Dirección:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDireccion" runat="server" TextMode="MultiLine" Rows="2" MaxLength="80" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                </div> 
                            </div>
                            <div class="row"></div>
                        </div>
                        <div class="card-footer" style="text-align: center;">
                            <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                OnClientClick="return alertConfirm(this, event, '¿Desea realizar el registro de los datos?', 'warning');">
                                <i class="fa fa-save"></i>
                                <span class="text">GUARDAR</span>
                            </asp:LinkButton>                                                   
                        </div> 
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>

    <script type="text/javascript">
        var controlId = '';

        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {               
            udpRegistroUpdate();
        });        
    </script>
</body>
</html>
