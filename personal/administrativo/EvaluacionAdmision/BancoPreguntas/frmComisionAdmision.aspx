<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmComisionAdmision.aspx.vb" Inherits="BancoPreguntas_frmComisionAdmision" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Registro de comisión de admisión ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/css-custom/bootstrap.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/css/bootstrap-select.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/fontawesome-5.2/css/all.min.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/css/bootstrap-datepicker.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.css>
    <!-- Estilos propios -->
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/style.css>
    <link rel="stylesheet" href=<%=ClsGlobales.PATH_CSS %>/comisionAdmision.css>
</head>

<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpParams" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddCod" Value="" runat="server" />
                <asp:HiddenField ID="hddTipoVista" Value="" runat="server" />
                <asp:HiddenField ID="hddParamsToastr" Value="" runat="server" />
                <!-- Parámetros para mensajes desde el servidor -->
                <asp:HiddenField ID="hddMenServMostrar" Value="false" runat="server" />
                <asp:HiddenField ID="hddMenServRpta" Value="1" runat="server" />
                <asp:HiddenField ID="hddMenServTitulo" Value="" runat="server" />
                <asp:HiddenField ID="hddMenServMensaje" Value="" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Registro de comisión de admisión<img src=<%=ClsGlobales.PATH_IMG %>/loading.gif id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-lista-tab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista" aria-selected="true">Lista</a>
                    <a class="nav-item nav-link disabled" id="nav-mantenimiento-tab" data-toggle="tab" href="#nav-mantenimiento" role="tab" aria-controls="nav-mantenimiento"
                        aria-selected="false">Mantenimiento</a>
                </div>
            </nav>
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="nav-lista" role="tabpanel" aria-labelledby="nav-lista-tab">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Filtros</h5>
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-sm-12 d-flex">
                                    <label for="cmbFiltroEstado" class="form-control-sm">Estado:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbFiltroEstado" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker">
                                            <asp:ListItem Text="TODOS" Value="-1" Selected="True" />
                                            <asp:ListItem Text="VIGENTES" Value="1" />
                                            <asp:ListItem Text="NO VIGENTES" Value="0" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12 d-flex">
                                    <label for="txtFiltroNroResolucion" class="form-control-sm">N° de Resolución:</label>
                                    <div class="fill">
                                        <asp:TextBox ID="txtFiltroNroResolucion" runat="server" CssClass="form-control form-control-sm" placeholder="N°" />
                                    </div>
                                </div>
                                <div class="col-sm-24 text-right">
                                    <asp:UpdatePanel ID="udpAcciones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light">
                                                <i class="fa fa-search"></i>
                                                <span class="text">Listar</span>
                                            </button>
                                            <button type="button" id="btnAgregar" runat="server" class="btn btn-accion btn-success ml-1">
                                                <i class="fa fa-plus"></i>
                                                <span class="text">Agregar</span>
                                            </button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <asp:UpdatePanel ID="udpGrvList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div id="divGrvList" runat="server">
                                                <hr class="separador">
                                                <asp:GridView ID="grvList" runat="server" DataKeyNames="codigo_cop" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="NRO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
                                                        <asp:BoundField DataField="nombreCompleto_Per" HeaderText="APELLIDOS Y NOMBRES" />
                                                        <asp:BoundField DataField="nombre_ccm" HeaderText="CARGO" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="nro_resolucion_cop" HeaderText="RESOLUCIÓN" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="ESTADO" ItemStyle-Width="15%" ItemStyle-HorizontalAlign=Center>
                                                            <ItemTemplate>
                                                                <%# IIf(Eval("vigente_cop"), "VIGENTE", "NO VIGENTE")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="OPERACIONES" ItemStyle-Width="25%" ItemStyle-HorizontalAlign=Center>
                                                            <ItemTemplate>
                                                                <button type="button" id="btnEditar" runat="server" class="btn btn-sm btn-accion btn-primary" title="Editar">
                                                                    <i class="fa fa-edit"></i>
                                                                    <span class="text">Editar</span>
                                                                </button>
                                                                <button type="button" id="btnFakeDesactivar" runat="server" class="btn btn-sm btn-accion btn-warning ml-1"
                                                                    Visible='<%# Eval("vigente_cop") %>' title="Desactivar">
                                                                    <i class="fa fa-ban"></i>
                                                                    <span class="text">Desactivar</span>
                                                                </button>
                                                                <button type="button" id="btnDesactivar" runat="server" class="d-none" />
                                                                <button type="button" id="btnFakeActivar" runat="server" class="btn btn-sm btn-accion btn-info ml-1" Visible='<%# Not Eval("vigente_cop") %>'
                                                                    title="Activar">
                                                                    <i class="fa fa-check-double"></i>
                                                                    <span class="text">Activar</span>
                                                                </button>
                                                                <button type="button" id="btnActivar" runat="server" class="d-none" />
                                                                <button type="button" id="btnFakeEliminar" runat="server" class="btn btn-sm btn-accion btn-danger ml-1" title="Eliminar">
                                                                    <i class="far fa-trash-alt"></i>
                                                                    <span class="text">Eliminar</span>
                                                                </button>
                                                                <button type="button" id="btnEliminar" runat="server" class="d-none" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                <div class="tab-pane fade" id="nav-mantenimiento" role="tabpanel" aria-labelledby="nav-lista-tab">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Registro / Edición</h5>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpMantenimiento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-48">
                                            <h1 class="title">Ingrese los datos del miembro de la comisión de admisión</h1>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row form-group mt-2">
                                        <label for="cmbPersonal" class="form-control-sm col-md-8">Miembro:</label>
                                        <div class="col-md-18">
                                            <asp:DropDownList ID="cmbPersonal" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker" data-live-search="true">
                                                <asp:ListItem Text="-- SELECCIONE --" Value="0" />
                                                <asp:ListItem Text="Docente A" Value="1" />
                                                <asp:ListItem Text="Docente B" Value="2" />
                                                <asp:ListItem Text="Docente C" Value="3" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbCargoComision" class="form-control-sm col-md-8">Cargo:</label>
                                        <div class="col-md-10">
                                            <asp:DropDownList ID="cmbCargoComision" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker">
                                                <asp:ListItem Text="-- SELECCIONE --" Value="0" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="txtNroResolucion" class="form-control-sm col-md-8">N° de Resolución:</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtNroResolucion" runat="server" CssClass="form-control form-control-sm" placeholder="N°" />
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="rbtControl5" class="form-control-sm col-md-8">Competencias:</label>
                                        <div class="col-md-14">
                                            <asp:ListBox ID="cmbCompetencia" runat="server" AutoPostBack="false" SelectionMode="Multiple" CssClass="form-control form-control-sm selectpicker">
                                            </asp:ListBox>
                                        </div>
                                    </div>
                                    <hr class="separador">
                                    <div class="row form-group mb-2">
                                        <div class="col-md-40 offset-md-8">
                                            <button type="button" id="btnCancelar" runat="server" class="btn btn-accion btn-light">
                                                <i class="fa fa-ban"></i>
                                                <span class="text">Cancelar</span>
                                            </button>
                                            <button type="button" id="btnGuardar" runat="server" class="btn btn-accion btn-primary ml-2">
                                                <i class="fa fa-save"></i>
                                                <span class="text">Guardar</span>
                                            </button>
                                            <button type="button" id="btnGuardarContinuar" runat="server" class="btn btn-accion btn-success ml-2">
                                                <i class="fa fa-save"></i>
                                                <span class="text">Guardar y Continuar</span>
                                            </button>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMensaje" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span id="spnMenServTitulo" runat="server" class="modal-title">Respuesta del Servidor</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="divMdlMensaje" class="alert alert-warning"></div>
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
    <script src="<%=ClsGlobales.PATH_ASSETS %>/jquery/jquery-3.3.1.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="<%=ClsGlobales.PATH_ASSETS %>/toastr-2.1.4-7/toastr.min.js"></script>
    <!-- Scripts propios -->
    <script src="<%=ClsGlobales.PATH_JS %>/funciones.js"></script>
    <script src="<%=ClsGlobales.PATH_JS %>/comisionAdmision.js"></script>

    <script type="text/javascript">
        var controlId = ''
        var controlsId = [];

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id
            controlsId = [];

            alternarLoadingGif('global', false);

            switch (controlId) {
                case 'btnListar':
                    controlsId.push(controlId);
                    break;
                case 'btnAgregar':
                    controlsId.push(controlId);
                    break;
                case 'btnCancelar':
                    controlsId.push(controlId);
                    controlsId.push('btnGuardar');
                    break;
                case 'btnGuardar':
                case 'btnGuardarContinuar':
                    controlsId.push('btnCancelar');
                    controlsId.push('btnGuardar');
                    controlsId.push('btnGuardarContinuar');
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1) {
                controlsId.push(controlId);
            }

            // Desactivo los botones
            controlsId.forEach(function (el) {
                atenuarBoton(el, false);
            });
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var error = args.get_error();
            if (error) {
                mostrarMensajeModal(-1, error);

                controlsId.forEach(function (el) {
                    atenuarBoton(el, true);
                });
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();

            var udpFormUpdated = false
            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;

                switch (udpPanelId) {
                    case 'udpGrvList':
                        initGrvTooltips();
                        break;
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);

            alternarLoadingGif('global', true);

            // Vuelvo a activar los botones
            controlsId.forEach(function (el) {
                atenuarBoton(el, true);
            });

            if (controlId.indexOf('btnDesactivar') > -1
                || controlId.indexOf('btnActivar') > -1
                || controlId.indexOf('btnEliminar') > -1) {
                accionConfirmadaFinalizada();
            }

            verificarParametros('TAB|MEN_SERV|TOASTR');
        });    
    </script>
</body>

</html>