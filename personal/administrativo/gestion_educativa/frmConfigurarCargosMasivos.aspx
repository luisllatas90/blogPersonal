<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfigurarCargosMasivos.aspx.vb" Inherits="administrativo_gestion_educativa_frmConfigurarCargosMasivos" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Configurar Cargos Masivos ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../assets/tempusdominus-bootstrap/css/tempusdominus-bootstrap-4.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/configurarCargosMasivos.css?x=1">
</head>

<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="scr" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddCod" Value="0" runat="server" />
                <asp:HiddenField ID="hddTipoVista" Value="L" runat="server" />
                <asp:HiddenField ID="hddParamsToastr" Value="" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Configurar Cargos Masivos<img src="img/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <nav>
                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                            <a class="nav-item nav-link active" id="nav-lista-tab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista"
                                aria-selected="true">Lista</a>
                            <a class="nav-item nav-link disabled" id="nav-mantenimiento-tab" data-toggle="tab" href="#nav-mantenimiento" role="tab"
                                aria-controls="nav-mantenimiento" aria-selected="false">Mantenimiento</a>
                        </div>
                    </nav>
                    <div class="tab-content" id="nav-tabContent">
                        <div class="tab-pane fade show active" id="nav-lista" role="tabpanel" aria-labelledby="nav-lista-tab">
                            <div class="row">
                                <div class="col-sm-48">
                                    <div class="card">
                                        <div class="card-header">
                                            <h5 class="title">Filtros</h5>
                                        </div>
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-sm-16 d-flex">
                                                    <label for="txtFiltroNombre" class="form-control-sm">Nombre:</label>
                                                    <div class="fill">
                                                        <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control form-control-sm" placeholder="Nombre del Evento" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-16 d-flex">
                                                </div>
                                                <div class="col-sm-16 text-right">
                                                    <asp:UpdatePanel ID="udpAcciones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                            <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-azul">
                                                                <i class="fa fa-search"></i>
                                                                <span class="text">Listar</span>
                                                            </button>
                                                            <button type="button" id="btnRegistrar" runat="server" class="btn btn-accion btn-verde">
                                                                <i class="fa fa-plus-square"></i>
                                                                <span class="text">Registrar</span>
                                                            </button>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-48">
                                                    <asp:UpdatePanel ID="udpConfiguracionPredefinidaCargo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                            <div id="divConfiguracionPredefinidaCargo" runat="server">
                                                                <hr>
                                                                <asp:GridView ID="grvConfiguracionPredefinidaCargo" runat="server" DataKeyNames="codigo_cpc"
                                                                    AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="nombre_cpc" HeaderText="NOMBRE" />
                                                                        <asp:BoundField HeaderText="ACCIONES" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="thead-dark" />
                                                                </asp:GridView>
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
                        <div class="tab-pane fade" id="nav-mantenimiento" role="tabpanel" aria-labelledby="nav-mantenimiento-tab">
                            <div class="card">
                                <div class="card-body">
                                    <asp:UpdatePanel ID="udpMantenimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-48">
                                                    <h1 class="main-title">Registrar/Actualizar Configuración<img src="img/loading.gif" id="loadingGif"></h1>
                                                    <hr>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <div class="col-xl-12 col-lg-15 d-flex">
                                                    <label for="cmbCicloAcademico" class="form-control-sm">Semestre:</label>
                                                    <div class="fill">
                                                        <asp:DropDownList ID="cmbCicloAcademico" runat="server" AutoPostBack="false"
                                                            CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xl-12 col-lg-15 d-flex">
                                                    <label for="cmbTipoProceso" class="form-control-sm">Tipo:</label>
                                                    <div class="fill">
                                                        <asp:DropDownList ID="cmbTipoProceso" runat="server" AutoPostBack="true"
                                                            CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xl-16 col-lg-20 d-flex">
                                                    <label for="txtNombre" class="form-control-sm">Nombre:</label>
                                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm" placeholder="Nombre" />
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <div class="col-xl-48">
                                                    <div class="card" id="cardConfigCargos">
                                                        <div class="card-header">
                                                            <h5 class="title">Configuración de Cargos</h5>
                                                        </div>
                                                        <div class="card-body">
                                                            <asp:UpdatePanel ID="udpIfrmConfigCargos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                <ContentTemplate>
                                                                    <iframe id="ifrmConfigCargos" runat="server" src="" width="100%" scrolling="no" frameborder="0"></iframe>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <div class="col-xl-48">
                                                    <div class="card" id="cardAlumnos">
                                                        <div class="card-header">
                                                            <h5 class="title">Alumnos</h5>
                                                        </div>
                                                        <div class="card-body">
                                                            <iframe id="ifrmFiltrosAlumno" runat="server" src="" width="100%" scrolling="no" frameborder="0"
                                                                style="z-index: 1000;"></iframe>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <div class="col-xl-48 text-right">
                                                    <asp:UpdatePanel ID="udpAccionesMantenimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                            <button type="button" id="btnCancelar" runat="server" class="btn btn-accion btn-light">
                                                                <i class="fa fa-ban"></i>
                                                                <span class="text">Cancelar</span>
                                                            </button>
                                                            <button type="button" id="btnFakeGuardar" class="btn btn-accion btn-azul">
                                                                <i class="fa fa-save"></i>
                                                                <span class="text">Guardar</span>
                                                            </button>
                                                            <button type="button" id="btnGuardar" runat="server" class="btn btn-accion btn-naranja d-none">
                                                                <i class="fa fa-save"></i>
                                                                <span class="text">Guardar</span>
                                                            </button>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
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
        <div id="mdlMensajeServidor" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <asp:UpdatePanel ID="udpMenServParametros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="divMenServParametros" runat="server" data-mostrar="false"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="modal-header">
                        <asp:UpdatePanel ID="udpMenServHeader" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <span id="spnMenServTitulo" runat="server" class="modal-title">Respuesta del
                                    Servidor</span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpMenServBody" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="divMenServMensaje" class="alert alert-warning" runat="server"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMensajeClienteConfirmar" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title">Confirmar Operación</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="divMensajeConfirmar"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" id="btnConfCancelar" data-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" id="btnConfContinuar">Continuar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="../../assets/moment/moment-with-locales.js"></script>
    <script src="../../assets/tempusdominus-bootstrap/js/tempusdominus-bootstrap-4.min.js"></script>
    <script src="../../assets/iframeresizer/iframeResizer.min.js"></script>
    <script src="../../assets/toastr-2.1.4-7/toastr.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/configurarCargosMasivos.js?x=1"></script>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            alternarLoadingGif('global', false);

            switch (controlId) {
                case 'btnListar':
                case 'btnRegistrar':
                    atenuarBoton(controlId, false);
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1) {
                atenuarBoton(controlId, false);
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var error = args.get_error();
            if (error) {
                // Manejar el error
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();

            var udpFormUpdated = false
            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;

                switch (udpPanelId) {
                    case 'udpIfrmConfigCargos':
                        initFrameConfigCargos();
                        break;
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);

            alternarLoadingGif('global', true);

            switch (controlId) {
                case 'btnListar':
                case 'btnRegistrar':
                    atenuarBoton(controlId, true);
                    break;
            }

            verificarCambiosAjax();
        });    
    </script>
</body>

</html>